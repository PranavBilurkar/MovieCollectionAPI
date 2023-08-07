using System.Collections.Generic;
using System.Linq;

namespace MovieCollectionAPI
{
    public class MovieCollectionService : IMovieCollectionService
    {
        private static List<Movie> movieCollection = new List<Movie>();

        public List<Movie> GetAllMovies() => movieCollection;

        public Movie GetMovieById(int id) => movieCollection.FirstOrDefault(m => m.Id == id);      

        public void AddMovie(Movie movie)
        {
            // Generate a unique ID for the new user (you can use an auto-incrementing ID or a GUID)
            movie.Id = movieCollection.Count > 0 ? movieCollection.Max(u => u.Id) + 1 : 1;

            movieCollection.Add(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            var existingMovie = movieCollection.FirstOrDefault(m => m.Id == movie.Id);
            if (existingMovie != null)
            {
                existingMovie.Title = movie.Title;
                existingMovie.Year = movie.Year;
                existingMovie.Genre = movie.Genre;
                existingMovie.Director = movie.Director;
            }
        }

        public List<Movie> SearchMovies(string keyword)
        {
            keyword = keyword.Trim().ToLowerInvariant();
            return movieCollection
                .Where(m => m.Title.ToLowerInvariant().Contains(keyword) || m.Genre.ToLowerInvariant().Contains(keyword))
                .ToList();
        }

        public void DeleteMovie(int id) => movieCollection.RemoveAll(m => m.Id == id);
    }
}
