using Serilog;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace MovieCollectionAPI
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous] // Allow access to this endpoint without authentication
        [Route("api/user/register")]
        public IHttpActionResult Register(User user)
        {
            Log.Information("Register endpoint called. Request received: {@User}", user);
            // Validate the user object
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Username and password are required.");
            }
            // Check if the username already exists in the repository
            var existingUser = _userService.GetUserByUsername(user.Username);
            if (existingUser != null)
            {
                Log.Warning("Username '{Username}' already exists. Registration failed.", user.Username);
                return BadRequest($"Username '{user.Username}' already exists.");
            }
            // Add the new user to the repository
            _userService.AddUser(user);

            Log.Information("User registered successfully. Username: {Username}", user.Username);

            // Return a success response
            return StatusCode(HttpStatusCode.Created);
        }

        [HttpPost]
        [AllowAnonymous] // Allow access to this endpoint without authentication
        [Route("api/user/login")]
        [ResponseType(typeof(string))] // The response will be a JWT token
        public IHttpActionResult Login(User user)
        {
            Log.Information("Login endpoint called. Request received: {@User}", user);
            // Validate the user object
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                Log.Warning("Invalid request. Username and password are required.");
                return BadRequest("Username and password are required.");
            }

            // Find the user by username
            var existingUser = _userService.GetUserByUsername(user.Username);

            // Check if the user exists and if the provided password matches the stored password
            if (existingUser == null || existingUser.Password != user.Password)
            {
                Log.Warning("Unauthorized access. Invalid username or password for user: {Username}", user.Username);
                return Unauthorized(); // Return a 401 Unauthorized response if the user is not authenticated
            }

            // In a real-world scenario, you would use a proper JWT authentication library to generate the token.
            // For simplicity, let's assume you have a custom method to generate the token for demonstration purposes.
            string token = JwtManager.GenerateToken(existingUser);

            Log.Information("User logged in successfully & Token is generated. Username: {Username}", user.Username);

            // Return the JWT token as a response
            return Ok(token);
        }

        [AllowAnonymous]
        public IHttpActionResult GetAllUsers()
        {
            Log.Information("GetAllUsers endpoint called.");
            var users = _userService.GetAllUsers();

            if (users == null || users.Count == 0)
            {
                Log.Warning("No users found.");
                return NotFound(); // Return a 404 Not Found response if there are no movies.
            }

            Log.Information("Number of users found: {UserCount}", users.Count);
            return Ok(users);
        }
    }
}