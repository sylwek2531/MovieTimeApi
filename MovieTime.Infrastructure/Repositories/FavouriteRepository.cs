
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

        public Favourite Get(Guid Id)
        {
            var favourities = _appDbContext.Favourities.First(c => c.ID == Id);
            return favourities;
        }

       
        public void Update(Favourite favourities)
        {
            _appDbContext.Entry(favourities).State = EntityState.Modified;
            _appDbContext.SaveChanges();
       }
        public void Delete(Favourite favourities)
        {
            _appDbContext.Remove(favourities);
            _appDbContext.SaveChanges();
         
        }
    }
}
