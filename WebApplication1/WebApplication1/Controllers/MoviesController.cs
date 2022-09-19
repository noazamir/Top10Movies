using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;






namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        //לצורך שימוש במחרוזת החיבור
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public MoviesController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select Top 10 MovieId,MovieName, MovieRating, MovieCategory,PhotoFileName
                            from dbo.movies
                            order by MovieRating desc
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoviesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{name}")]
        public JsonResult Get(string name)
        {
            string query = @"
                            select  MovieId,MovieName
                            from dbo.movies
                            where MovieName=@MovieName 
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoviesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MovieName", name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [Route("[action]/{category}")]
        [HttpGet]
        public JsonResult GetByCategory(string catagory)
        {
            string query = @"
                                select Top 10 MovieId,MovieName, MovieRating, MovieCategory,PhotoFileName
                                from dbo.movies
                                where MovieCategory=@MovieCategory
                                order by MovieRating desc
                                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoviesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MovieCategory", catagory);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(Movie mov)
        {
            string query = @"
                             insert into dbo.movies
                                (MovieName,MovieRating,MovieCategory,PhotoFileName)
                           values (@MovieName,@MovieRating,@MovieCategory,@PhotoFileName)";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoviesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MovieName", mov.MovieName);
                    myCommand.Parameters.AddWithValue("@MovieRating", mov.MovieRating);
                    myCommand.Parameters.AddWithValue("@MovieCategory", mov.MovieCategory);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", mov.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Movie mov)
        {
            string query = @"
                           update dbo.movies
                           set MovieName= @MovieName,
                            MovieRating=@MovieRating,
                            MovieCategory=@MovieCategory,
                            PhotoFileName=@PhotoFileName
                            where MovieId=@MovieId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoviesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MovieId", mov.MovieId);
                    myCommand.Parameters.AddWithValue("@MovieName", mov.MovieName);
                    myCommand.Parameters.AddWithValue("@MovieRating", mov.MovieRating);
                    myCommand.Parameters.AddWithValue("@MovieCategory", mov.MovieCategory);
                    myCommand.Parameters.AddWithValue("@PhotoFileName", mov.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.movies
                            where MovieId=@MovieId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoviesAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MovieId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }

    }
}
