using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class User : Entity
    {
        public string Name { get; protected set; }

        public string Surname { get; protected set; }

        public string Email { get; protected set; }

        public string Login { get; protected set; }

        public string Password { get; protected set; }

        public virtual ICollection<Favourite> Favourites { get; protected set; }

        public virtual ICollection<Movie> Movies{ get; protected set; }

        public User()
        {

        }
        public User(Guid id, string name, string surname, string email, string login, string password)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Password = password;

        }

        public void setName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"User can not have an empty name");
            }
            Name = name;

            UpdateAt = DateTime.Now;
        }
        public void setSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new Exception($"User can not have an empty surname");
            }
            Surname = surname;

            UpdateAt = DateTime.Now;
        }
        public void setEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"User can not have an empty email");
            }
            Email = email;

            UpdateAt = DateTime.Now;
        }
        public void setLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new Exception($"User can not have an empty login");
            }
            Login = login;

            UpdateAt = DateTime.Now;
        }
        public void setPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"User can not have an empty password");
            }
            Password = password;

            UpdateAt = DateTime.Now;
        }
    }
}
