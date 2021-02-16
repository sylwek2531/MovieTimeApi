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
using MovieTime.Infrastructure.Services;

namespace MovieTime.Api.Controllers
{
    [Authorize]
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
                    Login = user.Login,
                    Email = user.Email,
                    Token = tokenString
                });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistereDto model)
        {
            try
            {
                var newId = Guid.NewGuid();
                var user = _userService.Create(newId, model.Name, model.Surname, model.Email, model.Login, model.Password);
                return Created($"api/users/{newId}", user);

            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
