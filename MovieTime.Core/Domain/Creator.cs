﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Creator : Entity
    {

        public Guid Id_movie { get; protected set; }
        public string Name { get; protected set; }

        public Creator()
        {

        }
        public Creator(Guid id, Guid id_movie, string name)
        {
            Id = id;
            Name = name;
            Id_movie = id_movie;
        }

    }
}