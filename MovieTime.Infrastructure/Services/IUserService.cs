using MovieTime.Core.Domain;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
   public interface IUserService
    {
        UserDto Get(Guid Id);
        UserDto Create(Guid Id, string name, string surname, string email, string login, string password);
        void Update(Guid Id, string name, string surname, string email, string login, string password);
        void Delete(Guid Id);
        UserDto Authenticate(string login, string password);

    }
}
