using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MovieTime.Infrastructure.DTO;
using MovieTime.Infrastructure.Services;

namespace MovieTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public IConfiguration Configuration { get; }

        public MovieController(IMovieService movieService, IConfiguration configuration)
        {
            _movieService = movieService;
            Configuration = configuration;


        }
        [HttpGet]
        public IActionResult GetMovies()
        {
            try
            {
                IEnumerable<MovieDto> movies = _movieService.GetAll();
                return Ok(movies);
            }
            catch (ApplicationException ex)
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
                var movie = _movieService.Get(ID);
                return Ok(movie);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] MovieCreateDto movie)
        {
            try
            {
                var newId = Guid.NewGuid();
                var createMovie = _movieService.Create(newId, movie.UserID, movie.Title, movie.Description, movie.Year, movie.Creators, movie.Genres);
                return Created($"api/movies/{newId}", createMovie);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("id")]
        public IActionResult Put(Guid ID, [FromBody] MovieCreateDto movie)
        {
            try
            {
                _movieService.Update(ID, movie.Title, movie.Description, movie.Year, movie.Creators, movie.Genres);
                return NoContent();
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
            _movieService.Delete(ID);
            return NoContent();

        }

      /*  public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
*//*                    var filePath = Path.GetTempFileName();
*//*                    var filePath = Path.Combine(Configuration.GetSection("StoredFilesPath").Value,
          Path.GetRandomFileName());

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }*/
    }
}
