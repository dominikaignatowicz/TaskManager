using Microsoft.AspNetCore.Mvc;
using TaskManager.Server.AppDb;
using TaskManager.Server.DTOs;
using TaskManager.Server.Models;
using TaskManager.Server.ViewModel;

namespace TaskManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController :ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        public UserController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto registerUser)
        {
            var user = new User
            {
                Name = registerUser.Name + " " + registerUser.LastName,
                Email = registerUser.Email,
                Login = registerUser.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUser.Password),
            };

            _userService.CreateUserAsync(user);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDto loginUser)
        {
            var user = await _userService.AuthenticateUserAsync(loginUser);
            if (user == null)
                return Unauthorized("Nieprawidłowy login lub hasło");
            bool validPassword = BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password);

            if (!validPassword) return Unauthorized("Nieprawidłowy login lub hasło");
            var token = _jwtService.GenerateToken(user);
            return user;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var deletedUser = await _userService.DeleteUserAsync(id);
            return deletedUser ? NoContent() : NotFound();
        }
    }
}
