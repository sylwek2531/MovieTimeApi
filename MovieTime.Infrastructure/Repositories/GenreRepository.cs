
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
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _appDbContext;

        public GenreRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Genre Add(Genre genre)
        {
            _appDbContext.Add(genre);
            _appDbContext.SaveChanges();
            return genre;
        }

        public Genre Get(Guid ID)
        {
            var genre = _appDbContext.Genres.First(c => c.ID == ID);
            return genre;
        }

        public void Delete(Genre genre)
        {
            _appDbContext.Remove(genre);
            _appDbContext.SaveChanges();
         
        }
        public void DeleteByName(string name, Guid MovieID)
        {
            _appDbContext.Remove(_appDbContext.Genres.Single(g => g.MovieID == MovieID && g.Name == name));
            _appDbContext.SaveChanges();
        }
        public IEnumerable<Genre> GetAllByMovieId(Guid ID)
        {
            var genres = _appDbContext.Genres.Where(f => f.MovieID == ID);
            return genres;
        }
    }
}
