using Serilog;
using System;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace MovieCollectionAPI
{
    [JwtAuthentication]
    [CustomExceptionFilter]
    public class MovieController : ApiController
    {
        private readonly IMovieCollectionService _movieService;

        public MovieController(IMovieCollectionService movieService)
        {
            _movieService = movieService;
        }

       
        public IHttpActionResult GetAllMovies()
        {
            Log.Information("GetAllMovies endpoint called.");
            var movies = _movieService.GetAllMovies();

            if (movies == null || movies.Count == 0)
            {
                Log.Information("No movies found.");
                return NotFound(); // Return a 404 Not Found response if there are no movies.
            }

            return Ok(movies);
        }

     
        public IHttpActionResult GetMovieById(int id)
        {
            Log.Information("GetMovieById endpoint called with id: {MovieId}", id);
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
            {
                Log.Information("Movie with id {MovieId} not found.", id);
                return NotFound(); // Return a 404 Not Found response if the movie doesn't exist.
            }

            return Ok(movie);
        }       

        public IHttpActionResult Post([FromBody] Movie movie)
        {
            Log.Information("New Movie endpoint called. Request received: {@Movie}", movie);
            var userIdClaim = (ClaimsIdentity)User.Identity;
            var userId = userIdClaim.FindFirst(ClaimTypes.NameIdentifier);
           
            movie.OwnerId =Convert.ToInt32(userId.Value);
            Log.Information("Movie OwnerId set to: {OwnerId}", movie.OwnerId);
           
            _movieService.AddMovie(movie);
            Log.Information("Movie with ID {MovieId} created successfully.", movie.Id);
            
            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
        }

        public IHttpActionResult Put(int id, [FromBody] Movie updatedMovie)
        {
            Log.Information("Update Movie endpoint called. Request received: Movie ID = {MovieId}, Updated Movie = {@UpdatedMovie}", id, updatedMovie);
            if (updatedMovie == null || id != updatedMovie.Id)
            {
                Log.Warning("Invalid request. Movie ID in the request body does not match the ID in the URL. Request body: {@UpdatedMovie}", updatedMovie);
                return BadRequest("Invalid request."); // Return a 400 Bad Request if the movie data is invalid.
            }

            var existingMovie = _movieService.GetMovieById(id);
            if (existingMovie == null)
            {
                Log.Warning("Movie with ID {MovieId} not found.", id);
                return NotFound(); 
            }

            if (!AuthorizationHelper.IsAuthorized(existingMovie.OwnerId, User))
            {
                Log.Warning("Unauthorized access. User is not authorized to update the movie with ID {MovieId}.", id);
                return Unauthorized(); 
            }

            _movieService.UpdateMovie(updatedMovie);

            Log.Information("Movie with ID {MovieId} updated successfully.", id);

            return StatusCode(HttpStatusCode.NoContent);
        }

       
        public IHttpActionResult Delete(int id)
        {
            Log.Information("Delete Movie endpoint called.");
            var existingMovie = _movieService.GetMovieById(id);
            if (existingMovie == null)
            {
                return NotFound(); // Return a 404 Not Found if the movie with the given id doesn't exist.
            }

            if (!AuthorizationHelper.IsAuthorized(existingMovie.OwnerId, User))
            {
                return Unauthorized(); // Return a 401 Unauthorized if the user is not authorized.
            }

            _movieService.DeleteMovie(id);

            return StatusCode(HttpStatusCode.NoContent); // Return a 204 No Content response after successful deletion.
        }

        [HttpGet]
        public IHttpActionResult Search(string keyword)
        {
            Log.Information("Search Movie endpoint called.");
            var result = _movieService.SearchMovies(keyword);
            return Ok(result);
        }
    }
}
