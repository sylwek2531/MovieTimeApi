using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Creator : Entity
    {

        public Guid MovieID { get; protected set; }

        public string Name { get; protected set; }

        public virtual Movie Movie { get; set; }

        public Creator()
        {

        }
        public Creator(Guid id, Guid id_movie, string name)
        {
            ID = id;
            Name = name;
            MovieID = id_movie;
        }
        public void setName(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ApplicationException($"Movie crtaor not have an empty name");
            }
            Name = name;

            UpdateAt = DateTime.Now;
        }
        public void setMovieID(Guid movieID)
        {

            MovieID = movieID;

            UpdateAt = DateTime.Now;
        }
    }
}
