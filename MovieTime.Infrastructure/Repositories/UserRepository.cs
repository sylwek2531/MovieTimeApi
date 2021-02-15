
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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository (AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public User Add(User user)
        {
            _appDbContext.Add(user);
            _appDbContext.SaveChanges();
            return user;
        }

        public User Get(Guid Id)
        {
            var user = _appDbContext.Users.First(c => c.ID == Id);
            return user;
        }
        public void Update(User user)
        {
            _appDbContext.Entry(user).State = EntityState.Modified;
            _appDbContext.SaveChanges();
       }
        public void Delete(User user)
        {
            _appDbContext.Remove(user);
            _appDbContext.SaveChanges();
         
        }
    }
}
