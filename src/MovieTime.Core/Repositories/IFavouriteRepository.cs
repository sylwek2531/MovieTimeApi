using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Repositories
{
    public interface IFavouriteRepository
    {
        Favourite Get(Guid ID);
        Favourite Add(Favourite favourite);
        void Delete(Favourite favourite);
        IEnumerable<Favourite> GetAllByUserId(Guid ID);
        bool checkIfExistByData(Guid UserID, Guid MovieID);
        Favourite geByData(Guid UserID, Guid MovieID);
    }
}
