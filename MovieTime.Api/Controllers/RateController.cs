using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTime.Infrastructure.DTO;
using MovieTime.Infrastructure.Services;

namespace MovieTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateService _rateService;
        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [Authorize]
        [HttpGet("user_id")]
        public IActionResult GetAllGradesIdsByUserId(Guid UserID)
        {
            try
            {
                IEnumerable<RateDto> rate = _rateService.GetAllByUserId(UserID);
                return Ok(rate);
            }catch(ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("id")]
        public IActionResult Get(Guid ID)
        {
            var rate = _rateService.Get(ID);
            return Ok(rate);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] RateDto rate)
        {
            try
            {
                var newId = Guid.NewGuid();
                var rates = _rateService.Create(newId, rate.UserID, rate.MovieID, rate.Value);
                return Created($"api/rate/{newId}", rates);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("id")]
        public IActionResult Delete(Guid ID)
        {
            _rateService.Delete(ID);
            return NoContent();

        }
        [Authorize]
        [HttpPut("id")]
        public IActionResult Put(Guid ID, [FromBody] RateDto rate)
        {
            try
            {

                _rateService.Update(ID, rate.UserID, rate.MovieID, rate.Value);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
