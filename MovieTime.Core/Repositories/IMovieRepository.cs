using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Repositories
{
    public interface IMovieRepository
    {
        Movie Get(Guid Id);
        Movie Add(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
    }
}
