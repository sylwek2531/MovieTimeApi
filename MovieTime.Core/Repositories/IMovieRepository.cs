using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Repositories
{
    public interface IMovieRepository
    {
        Movie Get(Guid ID);
        Movie Add(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> Search(SearchOptions searchOptions);
        bool CheckMovieIfExistById(Guid ID);
        void updateRateMovie(Guid MovieID, int rateValue);
    }
}
