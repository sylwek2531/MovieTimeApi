using AutoMapper;
using MovieTime.Core.Domain;
using MovieTime.Core.Repositories;
using MovieTime.Infrastructure.DTO;
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
        public UserDto Create(Guid id, string name, string surname, string email, string login, string password)
        {
            var user = new User(id, name, surname, email, login, password);
            _userRepository.Add(user);
            return _mapper.Map<UserDto>(user);
        }

        public void Delete(Guid Id)
        {
            var user = _userRepository.Get(Id);
            _userRepository.Delete(user);
        }

        public UserDto Get(Guid Id)
        {
            var user = _userRepository.Get(Id);
            return _mapper.Map<UserDto>(user);
        }
        public void Update(Guid id, string name, string surname, string email, string login, string password)
        {
            var user = _userRepository.Get(id);
            user.setName(name);
            user.setSurname(surname);
            _userRepository.Update(user);
        }

    }
}
