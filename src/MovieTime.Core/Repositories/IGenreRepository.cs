using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Repositories
{
    public interface IGenreRepository
    {
        Genre Get(Guid ID);
        Genre Add(Genre genre);
        void Delete(Genre genre);
        void DeleteByMovieID(Guid MovieID);
        void DeleteByName(string name, Guid ID);
        IEnumerable<Genre> GetAllByMovieId(Guid ID);
    }
}
