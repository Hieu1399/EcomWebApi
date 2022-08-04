using EcomWebAPI.Models.User;
using EcomWebAPI.Service;
using EcomWebAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcomWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User userLogin)
        {
            var user =  _loginService.Authenticate(userLogin);
            if (user != null)
            {
                var accesstoken = _loginService.GenerateToken(user);
                var refreshToken = _loginService.GenerateRefreshToken(user);
                return Ok(new AuthenticatedUserResponse()
                {
                    AccessToken = accesstoken,
                    RefreshToken = refreshToken
                });
            }

            return NotFound("User not found");
        }

    }
}
