using System.Collections.Generic;

namespace MovieCollectionAPI
{
    public interface IMovieCollectionService
    {
        List<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        List<Movie> SearchMovies(string keyword);
        void DeleteMovie(int id);

    }
}