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
        public int Year { get; protected set; }

        public virtual User Users { get; set; }

        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Rated> Rateds { get; set; }
        public virtual ICollection<Creator> Creators { get; set; }

        public Movie()
        {

        }
        public Movie(Guid id, Guid id_user, string title, string description, int rate, int year)
        {
            ID = id;
            UserID= id_user;
            Title = title;
            Description = description;
            Rate = rate;
            Year = year;

        }
        public void setTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ApplicationException($"Movie can not have an empty Description");
            }
            Title = title;

            UpdateAt = DateTime.Now;
        }
        public void setRate(int rate)
        {
            if (string.IsNullOrWhiteSpace(rate.ToString()))
            {
                throw new ApplicationException($"Movie can not have an empty Title");
            }
            Rate = Rate;

            UpdateAt = DateTime.Now;
        }
        public void setDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ApplicationException($"Movie can not have an empty Title");
            }
            Description = description;

            UpdateAt = DateTime.Now;
        }
        public void setYear(int year)
        {
            if (string.IsNullOrWhiteSpace(year.ToString()))
            {
                throw new ApplicationException($"Movie can not have an empty year");
            }
            Year = year;
            UpdateAt = DateTime.Now;
        }
    }
}
