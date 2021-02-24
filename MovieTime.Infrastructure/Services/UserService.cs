using AutoMapper;
using MovieTime.Core.Domain;
using MovieTime.Core.Repositories;
using MovieTime.Infrastructure.DTO;
using MovieTime.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto Create(Guid ID, string name, string surname, string email, string login, string password)
        {

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new MovieTimeException("Password is required");
            }

            if (_userRepository.ValidateUserIfExistByLogin(login))
            {
                throw new MovieTimeException("Login \"" + login + "\" is already taken");
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            try
            {
                var user = new User(ID, name, surname, email, login, password, passwordHash, passwordSalt);
                var validUser = new ValidationHelper(user, "Problem with create a new user");
                validUser.ValidationModel();

                _userRepository.Add(user);
                return _mapper.Map<UserDto>(user);

            }
            catch (MovieTimeException ex)
            {
                throw new MovieTimeException(ex.getData, ex.Message);
            }
        }

        public void Delete(Guid ID)
        {
            var user = _userRepository.Get(ID);
            if (user != null)
            {
                _userRepository.Delete(user);

            }
            else
            {
                throw new MovieTimeException("User not exist");
            }

        }

        public UserDto Get(Guid ID)
        {
            var user = _userRepository.Get(ID);
            if (user == null)
            {
                throw new MovieTimeException("User not exist");
            }

            return _mapper.Map<UserDto>(user);
        }
        public void Update(Guid ID, string name, string surname, string email, string login, string password)
        {
            var user = _userRepository.Get(ID);
            if (user == null)
            {
                throw new MovieTimeException("User not found");
            }

            user.setName(name);
            user.setSurname(surname);

            if (!string.IsNullOrWhiteSpace(login))
            {
                var existUserWithLogin = _userRepository.ValidateUser(login);
                if (existUserWithLogin != null)
                {
                    if (existUserWithLogin.ID != ID)
                    {
                        throw new MovieTimeException("Login \"" + login + "\" is already taken");
                    }
                }
                else
                {
                    user.setLogin(login);
                }
            }



            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            try
            {
                var validUser = new ValidationHelper(user, "Problem with update a user");
                validUser.ValidationModel();
                _userRepository.Update(user);
            }
            catch (MovieTimeException gex)
            {
        
                throw new MovieTimeException(gex.getData, gex.Message);
            }

        }
        public UserDto Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.ValidateUser(login);

            // check if username exists
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return _mapper.Map<UserDto>(user);
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


    }
}
