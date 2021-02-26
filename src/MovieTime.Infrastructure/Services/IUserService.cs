using MovieTime.Core.Domain;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
   public interface IUserService
    {
        UserDto Get(Guid ID);
        UserDto Create(Guid ID, string name, string surname, string email, string login, string password);
        void Update(Guid ID, string name, string surname, string email, string login, string password);
        void Delete(Guid ID);
        UserDto Authenticate(string login, string password);

    }
}
