
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

        public MovieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Movie> GetAll()
        {
            List<Movie> movies = _appDbContext.Movies.ToList();
            foreach (var movie in movies)
            {
                movie.Creators = _appDbContext.Creators.Where(creator => creator.MovieID == movie.ID).ToList();
                movie.Genres = _appDbContext.Genres.Where(genre => genre.MovieID == movie.ID).ToList();
                movie.Rateds = _appDbContext.Rateds.Where(rate => rate.MovieID == movie.ID).ToList();
            }

            return movies;
        }
          public IEnumerable<Movie> GetAllByUserId(Guid ID)
        {
            var movies = _appDbContext.Movies.Where(f => f.UserID == ID);
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

        public IEnumerable<Movie> Search(SearchOptions searchOptions)
        {

            IEnumerable<Movie> moviesTitle = null;
            IEnumerable<Creator> creators = null;
            IEnumerable<Genre> genres = null;
            IEnumerable<Rated> popularMovies = null;

            if (searchOptions.Title != null)
            {
                moviesTitle = from m in _appDbContext.Movies where m.Title.Contains(searchOptions.Title) select m;
            }
            if (searchOptions.Creator != null)
            {
                creators = from c in _appDbContext.Creators where c.Name.Contains(searchOptions.Creator) select c;

            }
            if (searchOptions.Genre != null)
            {
                genres = from g in _appDbContext.Genres where g.Name.Contains(searchOptions.Genre) select g;

            }
            if ((int)searchOptions.Popular == 1)
            {
                popularMovies = from p in _appDbContext.Movies join rates in _appDbContext.Rateds on p.ID equals rates.MovieID select rates;
            }


            List<Movie> querry = new List<Movie>();
            if (moviesTitle != null)
            {
                 querry = moviesTitle.ToList();
            }
            else
            {
                querry = _appDbContext.Movies.ToList();
            }
            if(creators != null)
            {
               querry = querry.Join(creators, m => m.ID, c => c.MovieID, (m, c) => m).ToList();

            }
            if(genres != null)
            {
              querry =  querry.Join(genres, m => m.ID, g => g.MovieID, (m, g) => m).ToList();
            }
            if(popularMovies != null)
            {
               querry = querry.Join(popularMovies, m => m.ID, r => r.MovieID, (m, r) => m).ToList();
            }

            querry = querry.GroupBy(m => m.ID).ToDictionary(gdc => gdc.Key, gdc => gdc.First()).Values.ToList();
            // var secondFiveItems = myList.Skip(5).Take(5);
           
            if (searchOptions.Limit != 0)
            {
                querry = (List<Movie>)querry.Take(searchOptions.Limit).ToList();

            }
              
            return querry;
        }

    }
}
