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
        public readonly IGenreRepository _genreRepository;
        public readonly IUserRepository _userRepository;
        public readonly ICreatorRepository _creatorRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IUserRepository userRepository, IGenreRepository genreRepository,ICreatorRepository creatorRepository , IMapper mapper)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _genreRepository = genreRepository;
            _creatorRepository = creatorRepository;
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


        public MovieCreateDto Create(Guid ID, Guid UserID, string title, string description, int year, IEnumerable<string> creators, IEnumerable<string> genres)
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
            foreach (var genre in genres)
            {
                var genreItem = new Genre();
                genreItem.setMovieID(movie.ID);
                genreItem.setName(genre);
                _genreRepository.Add(genreItem);
            }
            foreach (var creator in creators)
            {
                var creatorItem = new Creator();
                creatorItem.setMovieID(movie.ID);
                creatorItem.setName(creator);
                _creatorRepository.Add(creatorItem);
            }

            return _mapper.Map<MovieCreateDto>(movie);
        }

        public void Update(Guid ID, string Title, string Description, int Year, IEnumerable<string> creators, IEnumerable<string> genres)
        {
            var movie = _movieRepository.Get(ID);
            if (movie == null)
            {
                throw new ApplicationException("Movie not found");
            }

            movie.setTitle(Title);
            movie.setDescription(Description);
            movie.setYear(Year);

           /* pobranie genre i creator na dpostawie movie id i aktuaclizavja, jesli jest wicecj genre lub cerate wtedy swtórz*/

         /*   foreach (var genre in genres)
          *   
            {
                var genreItem = new Genre();
                genreItem.setName(genre);
                _genreRepository.Add(genreItem);
            }
            foreach (var creator in creators)
            {
                var creatorItem = new Creator();
                creatorItem.setMovieID(movie.ID);
                creatorItem.setName(creator);
                _creatorRepository.Add(creatorItem);
            }*/

            _movieRepository.Update(movie);
        }


    }
}
