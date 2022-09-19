import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { variables } from './Variables.js';


const Movie = props => (
    <Link to={"/movie" + props.movie.MovieId}>{props.movie.MovieName}</Link>
)

export default function MoviesList(props) {

    const [movieId, SetMovieId] = useState(1);
    const [movieName, SetMovieName] = useState("");
    const [movieRating, SetMovieRating] = useState();
    const [movieCategory, SetMovieCategory] = useState("");
    const [PhotoFileName, SetPhotoFileName] = useState("anonymous.png");
    const [PhotoPath, SetPhotoPath] = useState([variables.PHOTO_URL]);
    const [movies, SetMovies] = useState([]);
    const categories = ["action", "Science Fiction", "Voltage", "comedy", "drama"];


    useEffect(() => {//מייבא את עשרת הסרטים הגבוהיים
        refreshList();
    }, []);

    const refreshList = () => {
        fetch(variables.API_URL + 'movies')
            .then(response => response.json())
            .then(data => {
                SetMovies(data);
                console.log(data)
            });
    }

    const createClick = () => {
        let isExsist = 0;

        //בדיקה האם כבר קיים הסרט
        fetch(variables.API_URL + 'movies/' + movieName)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                data.length == 0 ? isExsist = 0 : isExsist = 1;
            }, (error) => {
                alert('Failed');
            })
            .then(() => {
                if (isExsist == 0) {
                    fetch(variables.API_URL + 'movies', {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({
                            MovieName: movieName,
                            MovieRating: movieRating,
                            MovieCategory: movieCategory,
                            PhotoFileName: PhotoFileName

                        })
                    })
                        .then(res => res.json())
                        .then((result) => {
                            alert(result);
                            refreshList();
                            SetMovieId(0);

                        }, (error) => {
                            alert('Failedd');
                        })
                }
                else {
                    alert('This movie is exist!!')
                }
            })
    }

    const MovieList = () => {

        return movies.map((current, index) => {
            return <>
                <div className="uc_post_list_box container">
                    <div className="row">
                        <div className=" col-sm-2 col-lg-offset-2 col-lg-2">
                            <img loading="lazy" src={PhotoPath + current.PhotoFileName} width="100" height="100" />
                        </div>

                        <div className=" col-sm-2 col-lg-offset-2 col-lg-2">
                            <div className="uc_post_list_content_inside">

                                <div className="uc_post_list_title"><Movie movie={current} /></div>

                            </div>
                        </div>
                    </div>
                </div>

            </>
        })
    }

    const changeMovieName = e => {
        SetMovieName(e.target.value);
    }

    const changeMovieRating = (e) => {
        SetMovieRating(e.target.value);
    }
    const changeMovieCategory = (e) => {
        SetMovieCategory(e.target.value);
    }
    const imageUpload = (e) => {

        e.preventDefault();
        const formData = new FormData();
        formData.append("file", e.target.files[0], e.target.files[0].name);

        fetch(variables.API_URL + 'movies/savefile', {
            method: 'POST',
            body: formData
        })
            .then(res => res.json())
            .then(data => {
                SetPhotoFileName(data);
            })

    }

    const selectMovieByCategory = (e) => {
        console.log(e.target.value);
        const cat = e.target.value.JSON
        fetch(variables.API_URL + 'movies/GetByCategory/' + cat)
            .then(response => response.json())
            .then(data => {
                console.log(data);
            }, (error) => {
                alert('Failed');
            })

    }


    return (
        <div>

            <h1> top10 movies </h1>

            <button type="button"
                className="btn btn-primary float-end m-2"
                data-bs-toggle="modal"
                data-bs-target="#exampleModal"
                onClick={() => SetMovieId(0)}
            >Create new movie</button>

                <div className="d-flex flex-row bd-highlight mb-3">
                    <div className="p-2 w-30 bd-highlight">
                        <span className="input-group-text">Select by category</span>
                        <select className="form-select "
                            onChange={selectMovieByCategory}>
                            {/* <option >--Select by category--</option> */}
                            {categories.map(cat => <option >
                                {cat}
                            </option>)}
                        </select>
                    </div>
                </div>

            {MovieList()}

            {movieId == 0 ?
                <div id="exampleModal" className="modal fade" tabIndex="-1" >
                    <div className="modal-dialog modal-lg modal-dialog-centered">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">ceate movie</h5>

                            </div>

                            <div className="modal-body">
                                <div className="d-flex flex-row bd-highlight mb-3">

                                    <div className="p-2 w-50 bd-highlight">

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">movie Name</span>
                                            <input type="text" className="form-control"
                                                onChange={changeMovieName} />
                                        </div>

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">category</span>
                                            <select className="form-select"
                                                onChange={changeMovieCategory}>
                                                {categories.map(cat => <option >
                                                    {cat}
                                                </option>)}
                                            </select>
                                        </div>

                                        <div className="input-group mb-3">
                                            <span className="input-group-text">rating</span>
                                            <input type="number" className="form-control"
                                                onChange={changeMovieRating} />
                                        </div>
                                    </div>
                                    <div className="p-2 w-50 bd-highlight">
                                        <img width="250px" height="250px"
                                            src={PhotoPath + PhotoFileName} />
                                        <input className="m-2" type="file" onChange={imageUpload} />
                                    </div>
                                </div>
                                <button type="button"
                                    className="btn btn-primary float-start"
                                    onClick={() => createClick()} >Create</button>
                            </div>
                        </div>
                    </div>
                </div> : null}
        </div>
    );
}
