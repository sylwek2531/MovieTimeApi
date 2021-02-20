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
        MovieDto Create(Guid ID, Guid UserID, string Title, string Description, int Year);
        void Update(Guid ID, string Title, string Description, int Year);
        void Delete(Guid ID);
        IEnumerable<MovieDto> GetAll();

    }
}
