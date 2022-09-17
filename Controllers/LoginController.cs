using ApiAuth.Models;
using ApiAuth.Repositories;
using ApiAuth.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiAuth.Controllers
{
    [ApiController]
    [Route("users")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            // recover user
            var user = UserRepository.Get(model.Username, model.Password);

            // check user exists
            if (user == null)
                return NotFound(new { message = "Username or password is invalid" });

            // generate token
            var token = TokenService.GenerateToken(user);

            // hide pass
            user.Password = "";

            // return data
            return new { user = user, token = token };
        }
    }
}
