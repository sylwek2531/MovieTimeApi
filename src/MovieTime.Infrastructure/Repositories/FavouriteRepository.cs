
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
    public class FavouriteRepository : IFavouriteRepository
    {
        private readonly AppDbContext _appDbContext;

        public FavouriteRepository (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Favourite Add(Favourite favourities)
        {
            _appDbContext.Add(favourities);
            _appDbContext.SaveChanges();
            return favourities;
        }

        public Favourite Get(Guid ID)
        {
            var favourities = _appDbContext.Favourities.First(c => c.MovieID == ID);
            return favourities;
        }

        public void Delete(Favourite favourities)
        {
            _appDbContext.Remove(favourities);
            _appDbContext.SaveChanges();
         
        }
        public IEnumerable<Favourite> GetAllByUserId(Guid ID)
        {
            var favourities = _appDbContext.Favourities.Where(f => f.UserID == ID);
            return favourities;
        }
        public bool checkIfExistByData(Guid UserID, Guid MovieID)
        {
            var exist = _appDbContext.Favourities.Any(x => x.UserID == UserID && x.MovieID == MovieID);
            return exist;
        }
        public Favourite geByData(Guid UserID, Guid MovieID)
        {
            var favourite = _appDbContext.Favourities.First(c => c.UserID == UserID && c.MovieID == MovieID);
            return favourite;
        }
    }
}
