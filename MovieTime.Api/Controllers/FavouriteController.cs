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
    public class FavouriteController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;
        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }
        [HttpGet("id")]
        public IActionResult Get(Guid Id)
        {
            var favourite = _favouriteService.Get(Id);
            return Ok(favourite);
        }
        [HttpPost]
        public IActionResult Post([FromBody] FavouriteDto favourite)
        {
            var newId = Guid.NewGuid();
            favourite = _favouriteService.Create(newId, favourite.Id_movie, favourite.Id_user);
            return Created($"api/favourites/{newId}", favourite);
        }
        [HttpPut("id")]
        public IActionResult Put(Guid Id, [FromBody] FavouriteDto favourite)
        {
            _favouriteService.Update(Id, favourite.Id_movie, favourite.Id_user);
            return NoContent();
        }
        [HttpDelete("id")]
        public IActionResult Delete(Guid Id)
        {
            _favouriteService.Delete(Id);
            return NoContent();

        }
    }
}
