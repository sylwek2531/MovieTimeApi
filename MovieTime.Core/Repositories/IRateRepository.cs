using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Repositories
{
    public interface IRateRepository
    {
        Rated Get(Guid ID);
        Rated Add(Rated rate);
        void Update(Rated rate);
        void Delete(Rated rate);
        IEnumerable<Rated> GetAllByUserId(Guid ID);
        bool checkIfExistByData(Guid UserID, Guid MovieID);
        Rated geByData(Guid UserID, Guid MovieID);
        IEnumerable<Rated> GetAllRateByMovieID(Guid MovieID);
    }
}
