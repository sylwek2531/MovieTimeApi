using MovieTime.Core.Domain;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
   public interface IFavouriteService
    {
        FavouriteDto Get(Guid ID);
        FavouriteDto Create(Guid ID, Guid UserID, Guid MovieID);
        void Delete(Guid ID);
        IEnumerable<FavouriteDto> GetAllByUserId(Guid UserID);
    }
}
