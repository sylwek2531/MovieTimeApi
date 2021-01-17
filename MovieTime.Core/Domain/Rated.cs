using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Rated : Entity
    {

        public Guid Id_user { get; protected set; }
        public Guid Id_movie{ get; protected set; }
        public int Value{ get; protected set; }

        public Rated()
        {

        }
        public Rated(Guid id, Guid id_user, Guid id_movie, int value)
        {
            Id = id;
            Id_user = id_user;
            Id_movie = id_movie;
            Value = value;
        }

    }
}
