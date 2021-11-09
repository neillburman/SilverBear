using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.Configuration;
using Movies.Entities;

namespace Movies.Mappings
{
    public class MovieStatsClassMap : ClassMap<MovieStats>
    {
        public MovieStatsClassMap()
        {
            Map(m => m.MovieId).Name("movieId");
            Map(m => m.WatchDurationMs).Name("watchDurationMs");
        }
    }
}
