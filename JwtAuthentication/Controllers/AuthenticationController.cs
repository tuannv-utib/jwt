using JwtAuthentication.Models;
using JwtAuthentication.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtAuthentication.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserModel model)
        {
            var token = await _authenticationRepository.GennerateJwtToken(model);
            return string.IsNullOrEmpty(token) ? BadRequest(token) : Ok(token);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            return Ok(new UserModel());
        }
    }
}
