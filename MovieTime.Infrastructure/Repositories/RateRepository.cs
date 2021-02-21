
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
    public class RateRepository : IRateRepository
    {
        private readonly AppDbContext _appDbContext;

        public RateRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Rated Add(Rated rate)
        {
            _appDbContext.Add(rate);
            _appDbContext.SaveChanges();
            return rate;
        }
        public void Update(Rated rate)
        {
            _appDbContext.Update(rate);
            _appDbContext.SaveChanges();

        }

        public Rated Get(Guid ID)
        {
            var rate = _appDbContext.Rateds.Find(ID);
            return rate;
        }

        public void Delete(Rated rate)
        {
            _appDbContext.Remove(rate);
            _appDbContext.SaveChanges();
         
        }
        public IEnumerable<Rated> GetAllByUserId(Guid ID)
        {
            var rate = _appDbContext.Rateds.Where(f => f.UserID == ID);
            return rate;
        }
        public bool checkIfExistByData(Guid UserID, Guid MovieID)
        {
            var exist = _appDbContext.Rateds.Any(x => x.UserID == UserID && x.MovieID == MovieID);
            return exist;
        }
        public Rated geByData(Guid UserID, Guid MovieID)
        {
            var rate = _appDbContext.Rateds.First(c => c.UserID == UserID && c.MovieID == MovieID);
            return rate;
        }
        public IEnumerable<Rated> GetAllRateByMovieID(Guid MovieID)
        {
            var rates = _appDbContext.Rateds.Where(f => f.MovieID == MovieID);
            return rates;
        }
    }
}
