using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Favourite : Entity
    {

        public Guid MovieID { get; protected set; }

        public Guid UserID { get; protected set; }

        public virtual User User { get; set; }

        public virtual Movie Movie { get; set; }


        public Favourite()
        {

        }
        public Favourite(Guid id, Guid id_user, Guid id_movie)
        {
            ID = id;
            UserID = id_user;
            MovieID = id_movie;

        }

    }
}
