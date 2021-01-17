using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Favourite : Entity
    {

        public Guid Id_movie { get; protected set;}

        public Guid Id_user { get; protected set;}
/*        public virtual User User { get; set;}
*/

        public Favourite()
        {

        }
        public Favourite(Guid id, Guid id_user, Guid id_movie)
        {
            Id = id;
            Id_user = id_user;
            Id_movie = id_movie;
          
        }

    }
}
