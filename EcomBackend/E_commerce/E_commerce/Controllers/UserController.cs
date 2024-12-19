using E_commerce.Interface;
using E_commerce.Models;
using E_commerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {   
        private IUserService _userService;
        public UserController(IUserService userService) { 
          _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> Get() =>
        await _userService.GetAsync();


        [HttpPost("/RegisterUser")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
           await _userService.RegisterUserAsync(user);

            return CreatedAtAction(nameof(Get), new { id =user.Id }, user);
        }

        [HttpPost("/DeleteUser")]
        public async Task DeleteUser([FromBody] User user)
        {
            await _userService.UnRegisterUserAsync(user);
        }

        [HttpPost("Login")]

        public async Task<string> Login([FromBody] Login login)
        {
            var token = await _userService.GetTokenAsync(login.UserName,login.Password);
            return token;
        }
       

    }
}
