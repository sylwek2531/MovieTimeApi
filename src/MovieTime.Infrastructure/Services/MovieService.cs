using AutoMapper;
using MovieTime.Core.Domain;
using MovieTime.Core.Repositories;
using MovieTime.Infrastructure.DTO;
using MovieTime.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

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

            if (_movieRepository.CheckMovieIfExistById(ID))
            {
                var movie = _movieRepository.Get(ID);
                _movieRepository.Delete(movie);
            }
        }

        public MovieDto Get(Guid ID)
        {
            if (!_movieRepository.CheckMovieIfExistById(ID))
            {
                throw new MovieTimeException("Movie not exist");

            }
            var movie = _movieRepository.Get(ID);
            return _mapper.Map<MovieDto>(movie);
        }
          public IEnumerable<MovieDto> GetAllByUserId(Guid UserID)
        {
            if (!_userRepository.ValidateUserIfExistById(UserID))
            {
                throw new MovieTimeException("User not exist");

            }
            var movies = _movieRepository.GetAllByUserId(UserID);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public MovieDto Create(Guid ID, Guid UserID, string title, string description, int year, IEnumerable<string> creators, IEnumerable<string> genres, string bigPhoto, string mainPhoto)
        {
            if (UserID.ToString().Length > 0)
            {
                if (!_userRepository.ValidateUserIfExistById(UserID))
                {
                    throw new MovieTimeException("User not exist");
                }
            }
            else
            {
                UserID = Guid.Empty;
            }

            try
            {
                var movie = new Movie(ID, UserID, title, description, 0, year, bigPhoto, mainPhoto);
                var validMovie = new ValidationHelper(movie, "Problem with create a new movie");
                validMovie.ValidationModel();

                _movieRepository.Add(movie);

                foreach (var genre in genres)
                {
                    var genreItem = new Genre();
                    genreItem.setMovieID(movie.ID);
                    genreItem.setName(genre);
                    var validGenre = new ValidationHelper(genreItem, "Problem with genres");
                    try
                    {
                        //validGenre.ValidationModel();
                        _genreRepository.Add(genreItem);

                    }
                    catch (MovieTimeException gex)
                    {
                        _genreRepository.DeleteByMovieID(ID);
                        _movieRepository.Delete(movie);
                        throw new MovieTimeException(gex.getData, gex.Message);
                    }
                }
                foreach (var creator in creators)
                {
                    var creatorItem = new Creator();
                    creatorItem.setMovieID(movie.ID);
                    creatorItem.setName(creator);
                    var validCreator = new ValidationHelper(creatorItem, "Problem with creators");

                    try
                    {
                        // validCreator.ValidationModel();
                        _creatorRepository.Add(creatorItem);

                    }
                    catch (MovieTimeException gex)
                    {
                        _genreRepository.DeleteByMovieID(ID);
                        _movieRepository.Delete(movie);
                        _creatorRepository.DeleteByMovieID(ID);
                        throw new MovieTimeException(gex.getData, gex.Message);
                    }

                }

                var createMovie = Get(movie.ID);

                return _mapper.Map<MovieDto>(createMovie);
            }
            catch (MovieTimeException ex)
            {
                throw new MovieTimeException(ex.getData, ex.Message);
            }

        }

        public void Update(Guid ID, string Title, string Description, int Year, IEnumerable<string> creators, IEnumerable<string> genres, string bigPhoto, string mainPhoto)
        {
            var movie = _movieRepository.Get(ID);
            if (movie == null)
            {
                throw new MovieTimeException("Movie not found");
            }

            try
            {

                movie.setTitle(Title);
                movie.setDescription(Description);
                movie.setYear(Year);
                movie.setBigPhoto(bigPhoto);
                movie.setMainPhoto(mainPhoto);
                var validMovie = new ValidationHelper(movie, "Problem with update a movie");


                validMovie.ValidationModel();

                List<Genre> genresToSave = new List<Genre>();
                List<string> genresToDelete = new List<string>();

                List<Creator> creatorToSave = new List<Creator>();
                List<string> creatorToDelete = new List<string>();
                //////////
                ////////// GENRES
                //////////
                var getGenres = _genreRepository.GetAllByMovieId(ID).ToList();
                List<string> getNames = getGenres.Select(x => x.Name).ToList();
                List<string> genresList = genres.ToList();

                List<string> listGenres1 = getNames.Except(genresList).ToList();
                List<string> listGenres2 = genresList.Except(getNames).ToList();
                listGenres1.AddRange(listGenres2);

                foreach (var genre in listGenres1)
                {
                    if (getNames.Contains(genre))
                    {
                        genresToDelete.Add(genre);
                    }
                    else
                    {
                        var genreItem = new Genre();
                        genreItem.setMovieID(movie.ID);
                        genreItem.setName(genre);

                        genresToSave.Add(genreItem);

                    }
                }


                //////////
                ////////// CREATORS
                //////////
                //get data from db
                var getCreators = _creatorRepository.GetAllByMovieId(ID).ToList();

                //create lis of string
                List<string> getNamesCreatorsName = getCreators.Select(x => x.Name).ToList();

                //create list of string from data
                List<string> creatorList = creators.ToList();


                List<string> list1 = getNamesCreatorsName.Except(creatorList).ToList();
                List<string> list2 = creatorList.Except(getNamesCreatorsName).ToList();
                list1.AddRange(list2);


                foreach (var creator in list1)
                {
                    if (getNamesCreatorsName.Contains(creator))
                    {
                        creatorToDelete.Add(creator);
                    }
                    else
                    {
                        var creatorItem = new Creator();
                        creatorItem.setMovieID(movie.ID);
                        creatorItem.setName(creator);

                        creatorToSave.Add(creatorItem);
                    }
                }

                genresToSave.ForEach(g =>
                {
                    var validGenre = new ValidationHelper(g, "Problem with genres");
                    try
                    {
                        validGenre.ValidationModel();
                        _genreRepository.Add(g);

                    }
                    catch (MovieTimeException gex)
                    {
                        throw new MovieTimeException(gex.getData, gex.Message);
                    }

                });
                creatorToSave.ForEach(c =>
                {
                    var validCreator = new ValidationHelper(c, "Problem with creator");
                    try
                    {
                        validCreator.ValidationModel();
                        _creatorRepository.Add(c);

                    }
                    catch (MovieTimeException gex)
                    {
                        throw new MovieTimeException(gex.getData, gex.Message);
                    }
                });

                creatorToDelete.ForEach(c =>
                {
                    _creatorRepository.DeleteByName(c, movie.ID);

                });


                genresToDelete.ForEach(c =>
                {
                    _genreRepository.DeleteByName(c, movie.ID);

                });


                _movieRepository.Update(movie);
            }
            catch (MovieTimeException ex)
            {
                throw new MovieTimeException(ex.getData, ex.Message);
            }




        }

        public IEnumerable<MovieDto> GetSearch(SearchOptionsDTO searchOption)
        {
            var searchOptionDomain = new SearchOptions(searchOption.Title, searchOption.Limit, searchOption.Creator, searchOption.Genre, searchOption.Popular);
            var movies = _movieRepository.Search(searchOptionDomain);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);

        }
    }
}
