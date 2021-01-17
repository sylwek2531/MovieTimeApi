
using Microsoft.EntityFrameworkCore;
using MovieTime.Core.Domain;
using MovieTime.Core.Repositories;
using MovieTime.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTime.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _appDbContext;

        public MovieRepository (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Movie Add(Movie movie)
        {
            _appDbContext.Add(movie);
            _appDbContext.SaveChanges();
            return movie;
        }

        public Movie Get(Guid Id)
        {
            var movie = _appDbContext.Movies.First(c => c.Id == Id);
            return movie;
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Movie movie)
        {
            _appDbContext.Entry(movie).State = EntityState.Modified;
            _appDbContext.SaveChanges();
       }
        public void Delete(Movie movie)
        {
            _appDbContext.Remove(movie);
            _appDbContext.SaveChanges();
         
        }
    }
}
