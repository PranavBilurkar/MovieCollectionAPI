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
        [AllowAnonymous] 
        [Route("api/user/register")]
        public IHttpActionResult Register(User user)
        {
            Log.Information("Register endpoint called. Request received: {@User}", user);
            // Validate
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
            // Add the new user 
            _userService.AddUser(user);

            Log.Information("User registered successfully. Username: {Username}", user.Username);            
            return StatusCode(HttpStatusCode.Created);
        }

        [HttpPost]
        [AllowAnonymous] 
        [Route("api/user/login")]
        [ResponseType(typeof(string))] 
        public IHttpActionResult Login(User user)
        {
            Log.Information("Login endpoint called. Request received: {@User}", user);
            // Validate
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                Log.Warning("Invalid request. Username and password are required.");
                return BadRequest("Username and password are required.");
            }

            // Find the user by username
            var existingUser = _userService.GetUserByUsername(user.Username);

            // Check if the user exists
            if (existingUser == null || existingUser.Password != user.Password)
            {
                Log.Warning("Unauthorized access. Invalid username or password for user: {Username}", user.Username);
                return Unauthorized(); 
            }
           
            string token = JwtManager.GenerateToken(existingUser);
            Log.Information("User logged in successfully & Token is generated. Username: {Username}", user.Username);
          
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
                return NotFound(); 
            }

            Log.Information("Number of users found: {UserCount}", users.Count);
            return Ok(users);
        }
    }
}
