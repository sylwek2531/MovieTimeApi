using MovieTime.Core.Domain;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
    public interface IMovieService
    {
        MovieDto Get(Guid ID);
        MovieDto Create(Guid ID, Guid UserID, string Title, string Description, int Year, IEnumerable<string> creatorDto, IEnumerable<string> genreDto, string BigPhoto, string MainPhoto);
        void Update(Guid ID, string Title, string Description, int Year, IEnumerable<string> creatorDto, IEnumerable<string> genreDto, string BigPhoto, string MainPhoto);
        void Delete(Guid ID);
        IEnumerable<MovieDto> GetSearch(SearchOptionsDTO searchOption);
        IEnumerable<MovieDto> GetAll();

    }
}
