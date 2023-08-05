using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MovieCollectionAPI;

namespace MovieCollectionTest
{
    [TestClass]   
    public class MovieControllerTests
    {
        private MovieCollectionService movieRepository;

        [TestInitialize]
       
        public void TestInitialize()
        {
            // This method will be executed before each test method.
            // You can initialize any shared resources here.
            movieRepository = new MovieCollectionService();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // This method will be executed after each test method.
            // You can clean up any resources here.
            movieRepository = null;
        }

        [TestMethod]
        public void GetAllMovies_ShouldReturnAllMovies()
        {
            // Arrange
           
            var getMovie1 = new Movie { Id = 1, Title = "Tarzan", Year = 2021, Genre = "Action", Director = "abc" };
            var getMovie2 = new Movie { Id = 2, Title = "Spiderman", Year = 2022, Genre = "Comedy", Director = "pqr" };
            movieRepository.AddMovie(getMovie1);
            movieRepository.AddMovie(getMovie2);

            // Act
            var result = movieRepository.GetAllMovies();

            // Assert
            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, getMovie1);
            CollectionAssert.Contains(result, getMovie2);
        }

        [TestMethod]
        public void GetMovieById_ShouldReturnCorrectMovie()
        {
            // Arrange
           
            var movie1 = new Movie { Id = 1, Title = "Hunter", Year = 2021, Genre = "Sci", Director = "xyz" };
            movieRepository.AddMovie(movie1);

            // Act
            var result = movieRepository.GetMovieById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(movie1, result);
        }

        [TestMethod]
        public void GetMovieById_ShouldReturnNullIfMovieNotFound()
        {
            // Arrange
           

            // Act
            var result = movieRepository.GetMovieById(1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AddMovie_ShouldAddMovieToCollection()
        {
            // Arrange
           
            var movie = new Movie { Title = "Avengers", Year = 2023, Genre = "Tech", Director = "Director" };

            // Act
            movieRepository.AddMovie(movie);
            var result = movieRepository.GetAllMovies();

            // Assert
            Assert.AreEqual(1, result.Count);
            CollectionAssert.Contains(result, movie);
        }

        [TestMethod]
        public void UpdateMovie_ShouldUpdateExistingMovie()
        {
            // Arrange
           
            var movie = new Movie { Id = 1, Title = "Ved", Year = 2021, Genre = "Drama", Director = "ppb" };
            movieRepository.AddMovie(movie);

            var updatedMovie = new Movie { Id = 1, Title = "Updated Movie", Year = 2022, Genre = "Comedy", Director = "updated" };

            // Act
            movieRepository.UpdateMovie(updatedMovie);
            var result = movieRepository.GetMovieById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedMovie.Title, result.Title);
            Assert.AreEqual(updatedMovie.Year, result.Year);
            Assert.AreEqual(updatedMovie.Genre, result.Genre);
            Assert.AreEqual(updatedMovie.Director, result.Director);
        }

        [TestMethod]
        public void SearchMovies_ShouldReturnMatchingMovies()
        {
            // Arrange
           
            var movie1 = new Movie { Id = 1, Title = "Superman", Year = 2021, Genre = "Action", Director = "Director 1" };
            var movie2 = new Movie { Id = 2, Title = "Bawaal", Year = 2022, Genre = "Drama", Director = "Director 2" };
            var movie3 = new Movie { Id = 3, Title = "Kantara", Year = 2023, Genre = "Action", Director = "Director 3" };
            movieRepository.AddMovie(movie1);
            movieRepository.AddMovie(movie2);
            movieRepository.AddMovie(movie3);

            // Act
            var result = movieRepository.SearchMovies("Action");

            // Assert
            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, movie1);
            CollectionAssert.Contains(result, movie3);
        }

        [TestMethod]
        public void DeleteMovie_ShouldRemoveMovieFromCollection()
        {
            // Arrange
           
            var deleteMovie1 = new Movie { Id = 1, Title = "Kasam", Year = 2021, Genre = "Adventure", Director = "Bhat" };
            movieRepository.AddMovie(deleteMovie1);

            // Act
            movieRepository.DeleteMovie(1);
            var result = movieRepository.GetAllMovies();

            // Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
