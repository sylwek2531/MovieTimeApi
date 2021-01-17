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
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet("id")]
        public IActionResult Get(Guid Id)
        {
            var movie = _movieService.Get(Id);
            return Ok(movie);
        }
        [HttpPost]
        public IActionResult Post([FromBody] MovieDto movie)
        {
            var newId = Guid.NewGuid();
            movie = _movieService.Create(newId, movie.Id_user, movie.Title, movie.Description, movie.Rate);
            return Created($"api/movies/{newId}", movie);
        }
        [HttpPut("id")]
        public IActionResult Put(Guid Id, [FromBody] MovieDto movie)
        {
            _movieService.Update(Id, movie.Id_user, movie.Title, movie.Description, movie.Rate);
            return NoContent();
        }
        [HttpDelete("id")]
        public IActionResult Delete(Guid Id)
        {
            _movieService.Delete(Id);
            return NoContent();

        }
    }
}
