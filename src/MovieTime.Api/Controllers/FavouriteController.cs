using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTime.Infrastructure.DTO;
using MovieTime.Infrastructure.Helpers;
using MovieTime.Infrastructure.Services;

namespace MovieTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;
        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }

        [Authorize]
        [HttpGet("user_id")]
        public IActionResult GetAllFavouritesIdsByUserId(Guid UserID)
        {
            try
            {
                IEnumerable<FavouriteDto> favourites = _favouriteService.GetAllByUserId(UserID);
                return Ok(favourites);
            }catch(MovieTimeException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("movie_id")]
        public IActionResult Get(Guid ID)
        {
            var favourite = _favouriteService.Get(ID);
            return Ok(favourite);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] FavouriteDto favourite)
        {
            try
            {
                var newId = Guid.NewGuid();
                var favourites = _favouriteService.Create(newId, favourite.UserID, favourite.MovieID);
                return Created($"api/favourites/{newId}", favourites);
            }
            catch (MovieTimeException ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        [Authorize]
        [HttpDelete("id")]
        public IActionResult Delete(Guid ID)
        {
            _favouriteService.Delete(ID);
            return NoContent();

        }
    }
}
