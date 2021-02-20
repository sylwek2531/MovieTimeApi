using AutoMapper;
using MovieTime.Core.Domain;
using MovieTime.Core.Repositories;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public readonly IMovieRepository _movieRepository;
        public readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IUserRepository userRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public IEnumerable<MovieDto> GetAll()
        {
            var movies = _movieRepository.GetAll();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public void Delete(Guid ID)
        {

            var movie = _movieRepository.Get(ID);
            if (movie != null)
            {
                _movieRepository.Delete(movie);
            }
        }

        public MovieDto Get(Guid ID)
        {
            var movie = _movieRepository.Get(ID);
            if (movie == null)
            {
                throw new ApplicationException("Movie not exist");

            }
            return _mapper.Map<MovieDto>(movie);
        }


        public MovieDto Create(Guid ID, Guid UserID, string title, string description, int year)
        {
            if (UserID.ToString().Length > 0)
            {
                if (!_userRepository.ValidateUserIfExistById(UserID))
                {
                    throw new ApplicationException("User not exist");
                }
            }
            else
            {
                UserID = Guid.Empty;
            }
            var movie = new Movie(ID, UserID, title, description,0, year);

            _movieRepository.Add(movie);
            return _mapper.Map<MovieDto>(movie);
        }

        public void Update(Guid ID, string Title, string Description, int Year)
        {
            var movie = _movieRepository.Get(ID);
            if (movie == null)
            {
                throw new ApplicationException("Movie not found");
            }

            movie.setTitle(Title);
            movie.setDescription(Description);
            movie.setYear(Year);


            _movieRepository.Update(movie);
        }


    }
}
