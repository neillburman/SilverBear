using CsvHelper.Configuration;
using Movies.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Mappings
{
    public class MovieClassMap : ClassMap<Movie>
    {
        public MovieClassMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.MovieId).Name("MovieId");
            Map(m => m.Title).Name("Title");
            Map(m => m.Language).Name("Language");
            Map(m => m.Duration).Name("Duration");
            Map(m => m.ReleaseYear).Name("ReleaseYear");
        }
    }
}
