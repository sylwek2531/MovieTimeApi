using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Repositories
{
    public interface ICreatorRepository
    {
        Creator Get(Guid ID);
        Creator Add(Creator creator);
        void Delete(Creator creator);
        void DeleteByName(string name, Guid ID);
        void DeleteByMovieID(Guid MovieID);
        IEnumerable<Creator> GetAllByMovieId(Guid ID);
      
    }
}
