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
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }
        public MovieDto Create(Guid id, Guid id_user, string title, string description, int rate)
        {
            var movie = new Movie(id, id_user, title, description, rate);
            Console.WriteLine(movie);
            _movieRepository.Add(movie);
            return _mapper.Map<MovieDto>(movie);
        }
     
        public void Delete(Guid Id)
        {
            var movie = _movieRepository.Get(Id);
            _movieRepository.Delete(movie);
        }

        public MovieDto Get(Guid Id)
        {
            var movie = _movieRepository.Get(Id);
            return _mapper.Map<MovieDto>(movie);
        }

        public IEnumerable<MovieDto> GetAll(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, Guid Id_user, string Title, string Description, int Rate)
        {
            var movie = _movieRepository.Get(Id);
            movie.setTitle(Title);
          /*  movie.Id = Id;
            movie.Id_user = Id_user;
            movie.Title = Title;
            movie.Description = Description;
            movie.Rate = Rate;*/

            _movieRepository.Update(movie);
        }


    }
}
