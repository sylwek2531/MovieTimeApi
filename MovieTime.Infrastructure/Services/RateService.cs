using AutoMapper;
using MovieTime.Core.Domain;
using MovieTime.Core.Repositories;
using MovieTime.Infrastructure.DTO;
using MovieTime.Infrastructure.Helpers;
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
        public RateService(IRateRepository rateRepository, IUserRepository userRepository, IMovieRepository movieRepository, IMapper mapper)
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
                throw new MovieTimeException("User not exist");
            }
            if (!_movieRepository.CheckMovieIfExistById(MovieID))
            {
                throw new MovieTimeException("Movie not exist");
            }
            if (_rateRepository.checkIfExistByData(UserID, MovieID))
            {

                throw new MovieTimeException("Rate movie exist");
            }

            try
            {
                var rate = new Rated(ID, UserID, MovieID, Value);
                var validRate = new ValidationHelper(rate, "Problem with create a new rate");
                validRate.ValidationModel();
              var values =  _rateRepository.Add(rate);
                updateRateMovie(MovieID);
                return _mapper.Map<RateDto>(values);
            }
            catch (MovieTimeException ex)
            {
                throw new MovieTimeException(ex.getData, ex.Message);

            }
        }

        public void Delete(Guid ID)
        {
            var rate = _rateRepository.Get(ID);
            if (rate != null)
            {
                _rateRepository.Delete(rate);
            }
        }
        public IEnumerable<RateDto> GetAllByUserId(Guid UserID)
        {
            if (!_userRepository.ValidateUserIfExistById(UserID))
            {
                throw new MovieTimeException("User not exist");

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
            if (!_userRepository.ValidateUserIfExistById(UserID))
            {
                throw new MovieTimeException("User not exist");
            }
            if (!_movieRepository.CheckMovieIfExistById(MovieID))
            {
                throw new MovieTimeException("Movie not exist");
            }
          
            var rate = _rateRepository.Get(ID);
            if (rate == null)
            {
                throw new MovieTimeException("Movie not found");
            }
            try
            {

                rate.setValue(Value);
                var validRate = new ValidationHelper(rate, "Problem with update a rate");
                validRate.ValidationModel();

                _rateRepository.Update(rate);

                updateRateMovie(MovieID);


            }
            catch (MovieTimeException ex)
            {
                throw new MovieTimeException(ex.getData, ex.Message);
            }
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

                    if (rate.Value != 0)
                    {
                        count += rate.Value;
                    }
                }
                if (count > 0)
                {
                    avg = count / allRates.Count();
                }
                int rateMovie = (100 * avg) / 5;

                _movieRepository.updateRateMovie(MovieID, rateMovie);

            }
        }
    }
}
