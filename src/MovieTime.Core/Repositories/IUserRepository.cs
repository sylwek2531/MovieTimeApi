using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(Guid ID);
        User Add(User user);
        void Update(User user);
        void Delete(User user);
        User ValidateUser(string login);
        bool ValidateUserIfExistByLogin(string login);
        bool ValidateUserIfExistById(Guid ID);

    }
}
