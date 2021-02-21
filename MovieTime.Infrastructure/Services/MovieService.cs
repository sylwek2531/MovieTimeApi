using AutoMapper;
using MovieTime.Core.Domain;
using MovieTime.Core.Repositories;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public MovieService(IMovieRepository movieRepository, IUserRepository userRepository, IGenreRepository genreRepository, ICreatorRepository creatorRepository, IMapper mapper)
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


        public MovieDto Create(Guid ID, Guid UserID, string title, string description, int year, IEnumerable<string> creators, IEnumerable<string> genres)
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

            var movie = new Movie(ID, UserID, title, description, 0, year);

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

            var createMovie = Get(movie.ID);

            return _mapper.Map<MovieDto>(createMovie);
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

            if (genres.Count() > 0)
            {

                var getGenres = _genreRepository.GetAllByMovieId(ID).ToList();
                var getNames = getGenres.Select(x => x.Name).ToArray();

                var newElements = getNames.Except(genres.ToArray());

                foreach (var genre in newElements)
                {
                    if (!getNames.Contains(genre))
                    {
                        _genreRepository.DeleteByName(genre, movie.ID);
                    }
                    else
                    {
                        var genreItem = new Genre();
                        genreItem.setMovieID(movie.ID);
                        genreItem.setName(genre);
                        _genreRepository.Add(genreItem);
                    }
                }

            }



            if (creators.Count() > 0)
            {
                //get data from db
                var getCreators = _creatorRepository.GetAllByMovieId(ID).ToList();

                //create lis of string
                var getNamesCreatorsName = getCreators.Select(x => x.Name).ToList();

                //create list of string from data
                var creatorList = creators.ToList();



                List<string> getNamesCreators = getNamesCreatorsName.ToList();
                getNamesCreators.AddRange(creators.ToList());

                getNamesCreators.Distinct().ToList();
               // getNamesCreators.RemoveAll(item => creators.Contains(item));



                foreach (var creator in getNamesCreators)
                {
                    if (getNamesCreatorsName.Contains(creator))
                    {
                        _creatorRepository.DeleteByName(creator, movie.ID);
                    }
                    else
                    {
                        var creatorItem = new Creator();
                        creatorItem.setMovieID(movie.ID);
                        creatorItem.setName(creator);
                        _creatorRepository.Add(creatorItem);
                    }
                }
            }
            else
            {
                List<Creator> getAllCreators = _creatorRepository.GetAllByMovieId(ID).ToList();

                foreach (Creator creator in getAllCreators)
                {
                    _creatorRepository.Delete(creator);

                }
            }


            _movieRepository.Update(movie);
        }



    }
}
