using Movies.Entities;
using System.Collections.Generic;

namespace Movies.Repository
{
    public interface ICsvRepository
    {
        bool AddMovieData(PostMovieData movie);
        IEnumerable<ChosenMovie> GetMovie(int movieId);
        IEnumerable<ProjectMovieStats> GetMovieStats();
    }
}