using MovieTime.Core.Domain;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
   public interface IMovieService
    {
        MovieDto Get(Guid Id);
        MovieDto Create(Guid Id, Guid Id_user, string Title, string Description, int Rate);
        void Update(Guid Id, Guid Id_user, string Title, string Description, int Rate);
        void Delete(Guid Id);
/*        IEnumerable<MovieDto> GetAll(Guid Id);
*/
    }
}
