using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTime.Infrastructure.DTO;
using MovieTime.Infrastructure.Services;

namespace MovieTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("id")]
        public IActionResult Get(Guid Id)
        {
            if(Id == null || Id == Guid.Empty)
            {
                return NotFound("Parameter missing");
            }
            var user = _userService.Get(Id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Post([FromBody] UserDto user)
        {
            var newId = Guid.NewGuid();
            user = _userService.Create(newId, user.Name, user.Surname, user.Email, user.Login ,user.Password);
            return Created($"api/users/{newId}", user);
        }
        [HttpPut("id")]
        public IActionResult Put(Guid Id, [FromBody] UserDto user)
        {
            _userService.Update(Id, user.Name, user.Surname, user.Email, user.Login, user.Password);
            return NoContent();
        }
        [HttpDelete("id")]
        public IActionResult Delete(Guid Id)
        {
            _userService.Delete(Id);
            return NoContent();

        }
    }
}
