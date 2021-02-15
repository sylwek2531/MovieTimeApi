using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Movie : Entity
    {

        public Guid UserID { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public int Rate { get; protected set; }
        public virtual User Users { get; set; }

        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Rated> Rateds { get; set; }
        public virtual ICollection<Creator> Creators { get; set; }

        public Movie()
        {

        }
        public Movie(Guid id, Guid id_user, string title, string description, int rate)
        {
            ID = id;
            UserID= id_user;
            Title = title;
            Description = description;
            Rate = rate;
          
        }
        public void setTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new Exception($"Movie can not have an empty Title");
            }
            Title = title;

            UpdateAt = DateTime.Now;
        }

    }
}
