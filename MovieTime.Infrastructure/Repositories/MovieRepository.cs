
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
        public IEnumerable<Movie> GetAll()
        {
            List<Movie> movies =_appDbContext.Movies.ToList();
            foreach(var movie in movies)
            {
              movie.Creators = _appDbContext.Creators.Where(creator => creator.MovieID == movie.ID).ToList();
              movie.Genres = _appDbContext.Genres.Where(genre => genre.MovieID == movie.ID).ToList();
              movie.Rateds = _appDbContext.Rateds.Where(rate => rate.MovieID == movie.ID).ToList();
            }

            return movies;
        }
        public Movie Add(Movie movie)
        {
            _appDbContext.Add(movie);
            _appDbContext.SaveChanges();
            return movie;
        }

        public Movie Get(Guid ID)
        {
            var movie = _appDbContext.Movies.First(c => c.ID == ID);
            movie.Creators = _appDbContext.Creators.Where(creator => creator.MovieID == movie.ID).ToList();
            movie.Genres = _appDbContext.Genres.Where(genre => genre.MovieID == movie.ID).ToList();
            movie.Rateds = _appDbContext.Rateds.Where(rate => rate.MovieID == movie.ID).ToList();
            return movie;
        }

        public void Update(Movie movie)
        {
            _appDbContext.Movies.Update(movie);
            _appDbContext.SaveChanges();
       }
        public void Delete(Movie movie)
        {
            _appDbContext.Remove(movie);
            _appDbContext.SaveChanges();
         
        }
      
        public bool CheckMovieIfExistById(Guid ID)
        {
            return _appDbContext.Movies.Any(x => x.ID == ID);
        }
        public void updateRateMovie(Guid MovieID, int rateValue)
        {
            Movie movie = Get(MovieID);
            movie.setRate(rateValue);
            Update(movie);
        }

    }
}
