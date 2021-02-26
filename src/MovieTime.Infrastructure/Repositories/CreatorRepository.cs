
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
    public class CreatorRepository : ICreatorRepository
    {
        private readonly AppDbContext _appDbContext;

        public CreatorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Creator Add(Creator creator)
        {
            _appDbContext.Add(creator);
            _appDbContext.SaveChanges();
            return creator;
        }

        public Creator Get(Guid ID)
        {
            var creator = _appDbContext.Creators.First(c => c.ID == ID);
            return creator;
        }

        public void Delete(Creator creator)
        {
            _appDbContext.Creators.Remove(creator);
            _appDbContext.SaveChanges();
         
        }
        public void DeleteByName(string name, Guid MovieID)
        {
            var item = _appDbContext.Creators.Where(g => g.MovieID == MovieID && g.Name == name);
            if (item != null && item.Count() > 0)
            {
                _appDbContext.Creators.Remove((Creator)item.First());
                _appDbContext.SaveChanges();
            }
        }
        public void DeleteByMovieID(Guid MovieID)
        {
            _appDbContext.Creators.RemoveRange(_appDbContext.Creators.Where(g => g.MovieID == MovieID));
            _appDbContext.SaveChanges();
        }
        public IEnumerable<Creator> GetAllByMovieId(Guid ID)
        {
            var creators = _appDbContext.Creators.Where(f => f.MovieID == ID);
            return creators;
        }
    }
}
