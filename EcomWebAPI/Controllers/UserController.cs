using EcomWebAPI.Models.User;
using EcomWebAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EcomWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userservice;
        public UserController(ILogger<UserController> logger, IUserService userservicevice)
        {
            _logger = logger;
            _userservice = userservicevice;

        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAll()
        {
            var lstuser = await _userservice.GetUserList();
            return Ok(lstuser);
        }
        [HttpPost("signin")]
        [Authorize(Roles = "Administrator,Customer")]
        public async Task<IActionResult> CustomerSignin(User user)
        {
            var dbUser = await _userservice.UserExist(user.Username);
            if (dbUser)
            {
                ModelState.AddModelError("", "Username exist");
                return StatusCode(404, ModelState);
            }           
            if (ModelState.IsValid)
            {
                await _userservice.CreateCustomer(user);
                return Ok(user);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }
        [HttpPost("addadmin")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AdminSignin(User user)
        {
            var dbUser = await _userservice.UserExist(user.Username);
            if (dbUser)
            {
                ModelState.AddModelError("", "Username exist");
                return StatusCode(404, ModelState);
            }
            if (ModelState.IsValid)
            {
                await _userservice.CreateAdmin(user);
                return Ok(user);
            }
            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userservice.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var user = await _userservice.GetUserById(id);
            if (user == null)
                return BadRequest();
            await _userservice.Delete(user);
            return Ok("Delete Success!");

        }
    }
}
