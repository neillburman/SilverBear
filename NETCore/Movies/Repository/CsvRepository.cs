using CsvHelper;
using CsvHelper.Configuration;
using Movies.Entities;
using Movies.Mappings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Repository
{
    public class CsvRepository : ICsvRepository
    {

        IList<Movie> movies;
        IList<MovieStats> movieStats;

        public CsvRepository()
        {
            movies = GetAllMovies();
            movieStats = GetAllStats();
        }

        public IEnumerable<ChosenMovie> GetMovie(int movieId)
        {
            var chosenMovies = from m in movies.Where(m => m.MovieId == movieId).OrderByDescending(m => m.Id).OrderBy(m => m.Language)
                               select new ChosenMovie
                               {
                                   movieId = m.MovieId,
                                   title = m.Title,
                                   language = m.Language,
                                   releaseYear = m.ReleaseYear
                               };

            return chosenMovies;
        }

        public IEnumerable<ProjectMovieStats> GetMovieStats()
        {
            var movieStatsList = movieStats.GroupBy(s => s.MovieId, (key, g) => new MovieStatsResults { MovieId = key, MovieWatches = g.Count(), Total = g.Sum(t => t.WatchDurationMs / 1000), Average = g.Sum(t => t.WatchDurationMs / 1000) / g.Count() });

            //Returns a list of movie properties per movieId 
            var distinctMovieList = movies.GroupBy(o => new { o.MovieId }).Select(o => o.FirstOrDefault()).Select(m => new DistinctMovie { MovieId = m.MovieId, Title = m.Title, Release = m.ReleaseYear });

            var movieStatsData = from ms in movieStatsList
                                 join dml in distinctMovieList
                                 on ms.MovieId equals dml.MovieId

                                 select new ProjectMovieStats
                                 {
                                     movieId = dml.MovieId,
                                     title = dml.Title,
                                     averageWatchDurationS = ms.Average,
                                     watches = ms.MovieWatches,
                                     releaseYear = dml.Release
                                 };
            return movieStatsData;
        }

        public bool AddMovieData(PostMovieData movie)
        {
            var records = new List<PostMovieData>();
            records.Add(movie);

            // Append to the file.
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            // string currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string filePath = System.IO.Path.GetFullPath("metadata.csv");
            using (var stream = System.IO.File.Open(filePath, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(records);
            }
            return true;
        }


        private static List<Movie> GetAllMovies()
        {

            string filePath = System.IO.Path.GetFullPath("metadata.csv");
            using (var streamReader = new StreamReader(filePath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<MovieClassMap>();

                    return csvReader.GetRecords<Movie>().ToList();
                }
            }
        }


        private static List<MovieStats> GetAllStats()
        {
            string filePath = System.IO.Path.GetFullPath("stats.csv");
            using (var streamReader = new StreamReader(filePath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<MovieStatsClassMap>();

                    return csvReader.GetRecords<MovieStats>().ToList();
                }
            }
        }
    }
}
