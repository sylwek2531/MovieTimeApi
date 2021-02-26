using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieTime.Infrastructure.DTO;
using MovieTime.Infrastructure.Helpers;
using MovieTime.Infrastructure.Services;

namespace MovieTime.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public UserController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateDto model)
        {
            var user = _userService.Authenticate(model.Login, model.Password);
            if (user == null)
                return BadRequest(new { message = "Usernme or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenData = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.ID.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenData);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(
                new
                {
                    User = user,
                    Token = tokenString
                });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistereDto register)
        {
            try
            {
                var newId = Guid.NewGuid();
                var user = _userService.Create(newId, register.Name, register.Surname, register.Email, register.Login, register.Password);
                return Created($"api/users/{newId}", user);

            }
            catch (MovieTimeException ex)
            {
                return BadRequest(new { message = ex.Message, data = ex.getData });
            }
        }

        [Authorize]
        [HttpPut("id")]
        public IActionResult Put(Guid ID, [FromBody] UserUpdateDto update)
        {
            try
            {
                _userService.Update(ID, update.Name, update.Surname, update.Email, update.Login, update.Password);

                return NoContent();
            }
            catch (MovieTimeException ex)
            {
                return BadRequest(new { message = ex.Message, data = ex.getData });
            }
        }

        [Authorize]
        [HttpDelete("id")]
        public IActionResult Delete(Guid ID)
        {
            try
            {
                _userService.Delete(ID);
                return NoContent();
            }
            catch (MovieTimeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [Authorize]
        [HttpGet("id")]
        public IActionResult Get(Guid ID)
        {
            try
            {
                var movie = _userService.Get(ID);
                return Ok(movie);
            }
            catch (MovieTimeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
