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
        IEnumerable<Genre> GetAllByMovieId(Guid ID);
    }
}
