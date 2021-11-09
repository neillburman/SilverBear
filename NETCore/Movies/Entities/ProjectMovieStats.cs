﻿namespace Movies.Entities
{
    public  class ProjectMovieStats
    {
        public int movieId { get; set; }
        public string title { get; set; }
        public int averageWatchDurationS { get; set; }
        public int watches { get; set; }
        public int releaseYear { get; set; }
    }
}