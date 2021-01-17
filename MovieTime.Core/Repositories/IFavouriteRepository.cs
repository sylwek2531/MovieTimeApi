using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Repositories
{
    public interface IFavouriteRepository
    {
        Favourite Get(Guid Id);
        Favourite Add(Favourite movie);
        void Update(Favourite movie);
        void Delete(Favourite movie);
    }
}
