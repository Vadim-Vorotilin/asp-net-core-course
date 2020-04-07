using System.Linq;
using System.Threading.Tasks;
using LoginApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<MyUser> _signInManager;
        private readonly UserManager<MyUser> _userManager;
        private readonly JwtService _jwtService;

        public LoginController(SignInManager<MyUser> signInManager,
                               UserManager<MyUser> userManager,
                               JwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }
        
        [HttpGet("token")]
        public async Task<IActionResult> GetToken(string username, string password)
        {
            // Go To Identity
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (!result.Succeeded)
                return Unauthorized("User name or password isn't correct");

            var lastname = await _userManager.Users
                                             .Where(u => u.UserName == username)
                                             .Select(u => u.LastName)
                                             .FirstAsync();

            var token = _jwtService.GetToken(username, lastname);

            return Ok(token);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromForm] string username,
                                                    [FromForm] string lastname,
                                                    [FromForm] string password)
        {
            var result = await _userManager.CreateAsync(new MyUser
                                                        {
                                                            UserName = username,
                                                            LastName = lastname
                                                        },
                                                        password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);
            
            return Ok(username);
        }
    }
}