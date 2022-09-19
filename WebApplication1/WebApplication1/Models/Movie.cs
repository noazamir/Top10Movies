using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }

        public int MovieRating { get; set; }

        public string MovieCategory { get; set; }

        public string PhotoFileName { get; set; }
    }
}
