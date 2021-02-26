using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class User : Entity, IValidatableObject
    {
        [Required]
        [MinLength(2)]
        public string Name { get; protected set; }

        [Required]
        [MinLength(2)]
        public string Surname { get; protected set; }

        [Required]
        [EmailAddress]
        [MinLength(2)]
        public string Email { get; protected set; }

        [Required]
        [MinLength(2)]
        public string Login { get; protected set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; protected set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Favourite> Favourites { get; protected set; }

        public virtual ICollection<Movie> Movies{ get; protected set; }

        public User()
        {

        }
        public User(Guid id, string name, string surname, string email, string login, string password, byte[] passwordHash, byte[] passwordSalt)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Password = password;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;

        }

        public void setName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ApplicationException($"User can not have an empty name");
            }
            Name = name;

            UpdateAt = DateTime.Now;
        }
        public void setSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new ApplicationException($"User can not have an empty surname");
            }
            Surname = surname;

            UpdateAt = DateTime.Now;
        }
        public void setEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ApplicationException($"User can not have an empty email");
            }
            Email = email;

            UpdateAt = DateTime.Now;
        }
        public void setLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ApplicationException($"User can not have an empty login");
            }
            Login = login;

            UpdateAt = DateTime.Now;
        }
        public void setPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ApplicationException($"User can not have an empty password");
            }
            Password = password;

            UpdateAt = DateTime.Now;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(this.Name,
                new ValidationContext(this, null, null) { MemberName = "Name" },
                results);
            Validator.TryValidateProperty(this.Surname,
                new ValidationContext(this, null, null) { MemberName = "Surname" },
                results);
            Validator.TryValidateProperty(this.Email,
              new ValidationContext(this, null, null) { MemberName = "Email" },
              results);
            Validator.TryValidateProperty(this.Login,
              new ValidationContext(this, null, null) { MemberName = "Login" },
              results);
            Validator.TryValidateProperty(this.Password,
               new ValidationContext(this, null, null) { MemberName = "Password" },
               results);

            /*   // some other random test
               if (this.Prop1 > this.Prop2)
               {
                   results.Add(new ValidationResult("Prop1 must be larger than Prop2"));
               }*/

            return results;
        }
    }
}
