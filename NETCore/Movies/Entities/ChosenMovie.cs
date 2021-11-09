using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Entities
{
    public class ChosenMovie
    {
        public int movieId { get; set; }
        public string title { get; set; }
        public string language { get; set; }
        public int releaseYear { get; set; }
    }
}
