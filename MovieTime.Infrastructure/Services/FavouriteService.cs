using AutoMapper;
using MovieTime.Core.Domain;
using MovieTime.Core.Repositories;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
    public class FavouriteService : IFavouriteService
    {
        public readonly IFavouriteRepository _favouriteRepository;
        public readonly IUserRepository _userRepository;
        public readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public FavouriteService(IFavouriteRepository favouriteRepository, IUserRepository userRepository,IMovieRepository movieRepository, IMapper mapper)
        {
            _favouriteRepository = favouriteRepository;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }
        public FavouriteDto Create(Guid ID, Guid UserID, Guid MovieID)
        {
            if (!_userRepository.ValidateUserIfExistById(UserID))
            {
                throw new ApplicationException("User not exist");
            }
            if (!_movieRepository.CheckMovieIfExistById(MovieID))
            {
                throw new ApplicationException("Movie not exist");
            }
            if (_favouriteRepository.checkIfExistByData(UserID, MovieID))
            {
                /* var getExist = _favouriteRepository.geByData(Id_user, Id_movie);
                 return Delete(getExist.ID);*/
                throw new ApplicationException("Favourite movie exist");
            }

            var favourite = new Favourite(ID, UserID, MovieID);
            _favouriteRepository.Add(favourite);
            return _mapper.Map<FavouriteDto>(favourite);
        }
     
        public void Delete(Guid ID)
        {
            var favourite = _favouriteRepository.Get(ID);
            if(favourite != null)
            {
            _favouriteRepository.Delete(favourite);
            }
        }
        public IEnumerable<FavouriteDto> GetAllByUserId(Guid UserID)
        {
            if (!_userRepository.ValidateUserIfExistById(UserID))
            {
                throw new ApplicationException("User not exist");

            }
            var favourities = _favouriteRepository.GetAllByUserId(UserID);
            return _mapper.Map<IEnumerable<FavouriteDto>>(favourities);
        }
        public FavouriteDto Get(Guid ID)
        {
            var favourite = _favouriteRepository.Get(ID);
            return _mapper.Map<FavouriteDto>(favourite);
        }


    }
}
