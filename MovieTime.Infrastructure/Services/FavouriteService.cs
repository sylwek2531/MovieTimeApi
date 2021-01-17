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
        private readonly IMapper _mapper;
        public FavouriteService(IFavouriteRepository favouriteRepository, IMapper mapper)
        {
            _favouriteRepository = favouriteRepository;
            _mapper = mapper;
        }
        public FavouriteDto Create(Guid Id, Guid Id_user, Guid Id_movie)
        {
            var favourite = new Favourite(Id, Id_user, Id_movie);
            _favouriteRepository.Add(favourite);
            return _mapper.Map<FavouriteDto>(favourite);
        }
     
        public void Delete(Guid Id)
        {
            var favourite = _favouriteRepository.Get(Id);
            _favouriteRepository.Delete(favourite);
        }

        public FavouriteDto Get(Guid Id)
        {
            var favourite = _favouriteRepository.Get(Id);
            return _mapper.Map<FavouriteDto>(favourite);
        }

        public IEnumerable<FavouriteDto> GetAll(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid Id, Guid Id_user, Guid Id_movie)
        {
            var favourite = _favouriteRepository.Get(Id);

            _favouriteRepository.Update(favourite);
        }


    }
}
