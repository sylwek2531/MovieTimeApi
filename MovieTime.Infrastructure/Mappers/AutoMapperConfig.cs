﻿using AutoMapper;
using MovieTime.Core.Domain;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize() => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>();
            cfg.CreateMap<Movie, MovieDto>();
            cfg.CreateMap<Movie, MovieCreateDto>();
            cfg.CreateMap<Genre, GenreDto>();
            cfg.CreateMap<Creator, CreatorDto>();
            cfg.CreateMap<Favourite, FavouriteDto>();
            cfg.CreateMap<Rated, RateDto>();

        }).CreateMapper();
    }
}
