using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(Guid Id);
        IEnumerable<User> GetAll();
        User Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}
