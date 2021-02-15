using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Rated : Entity
    {

        public Guid UserID { get; protected set; }
        public Guid MovieID { get; protected set; }
        public int Value { get; protected set; }
        public virtual User User { get; set; }

        public virtual Movie Movie { get; set; }
        public Rated()
        {

        }
        public Rated(Guid id, Guid id_user, Guid id_movie, int value)
        {
            ID = id;
            UserID = id_user;
            MovieID = id_movie;
            Value = value;
        }

    }
}
