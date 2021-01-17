using MovieTime.Core.Domain;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
   public interface IFavouriteService
    {
        FavouriteDto Get(Guid Id);
        FavouriteDto Create(Guid Id, Guid Id_user, Guid Id_movie);
        void Update (Guid Id, Guid Id_user, Guid Id_movie);
        void Delete(Guid Id);
        IEnumerable<FavouriteDto> GetAll(Guid Id);

    }
}
