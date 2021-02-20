using AutoMapper;
using MovieTime.Core.Domain;
using MovieTime.Core.Repositories;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
    public class RateService : IRateService
    {
        public readonly IRateRepository _rateRepository;
        public readonly IUserRepository _userRepository;
        public readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public RateService(IRateRepository rateRepository, IUserRepository userRepository,IMovieRepository movieRepository, IMapper mapper)
        {
            _rateRepository = rateRepository;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }
        public RateDto Create(Guid ID, Guid UserID, Guid MovieID, int Value)
        {
            if (!_userRepository.ValidateUserIfExistById(UserID))
            {
                throw new ApplicationException("User not exist");
            }
            if (!_movieRepository.CheckMovieIfExistById(MovieID))
            {
                throw new ApplicationException("Movie not exist");
            }
            if (_rateRepository.checkIfExistByData(UserID, MovieID))
            {
                /* var getExist = _favouriteRepository.geByData(Id_user, Id_movie);
                 return Delete(getExist.ID);*/
                throw new ApplicationException("Rate movie exist");
            }

            var rate = new Rated(ID, UserID, MovieID, Value);
            _rateRepository.Add(rate);
            return _mapper.Map<RateDto>(rate);
        }
     
        public void Delete(Guid ID)
        {
            var rate = _rateRepository.Get(ID);
            if(rate!= null)
            {
                _rateRepository.Delete(rate);
            }
        }
        public IEnumerable<RateDto> GetAllByUserId(Guid UserID)
        {
            if (!_userRepository.ValidateUserIfExistById(UserID))
            {
                throw new ApplicationException("User not exist");

            }
            var rate = _rateRepository.GetAllByUserId(UserID);
            return _mapper.Map<IEnumerable<RateDto>>(rate);
        }
        public RateDto Get(Guid ID)
        {
            var rate = _rateRepository.Get(ID);
            return _mapper.Map<RateDto>(rate);
        }
        public void Update(Guid ID, Guid UserID, Guid MovieID, int Value)
        {
            var rate = _rateRepository.Get(ID);
            if (rate == null)
            {
                throw new ApplicationException("Movie not found");
            }

            rate.setValue(Value);


            _rateRepository.Update(rate);
        }

        private void updateRateMovie(Guid MovieID)
        {
            if (_movieRepository.CheckMovieIfExistById(MovieID))
            {
               var allRates = _rateRepository.GetAllRateByMovieID(MovieID);
                int avg = 0;
                int count = 0;
                foreach (Rated rate in allRates)
                {
                    
                    if(rate.Value != 0)
                    {
                        count += rate.Value;
                    }
                }
                if(count > 0)
                {
                    avg = count / allRates.Count();
                }
                int rateMovie = (100 * avg) / 5;

                _movieRepository.updateRateMovie(MovieID, rateMovie);

            }
        } 
    }
}
