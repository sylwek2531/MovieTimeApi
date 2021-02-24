using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Genre : Entity, IValidatableObject
    {

        public Guid MovieID { get; protected set; }
        [Required]
        [MinLength(2)]
        public string Name { get; protected set; }
        public virtual Movie Movie { get; set; }

        public Genre()
        {

        }
        public Genre(Guid id, Guid id_movie, string name)
        {
            ID = id;
            Name = name; 
            MovieID = id_movie;
        }
        public void setName(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ApplicationException($"Movie genre not have an empty name");
            }
            Name = name;

            UpdateAt = DateTime.Now;
        }
        public void setMovieID(Guid movieID)
        {

         
            MovieID = movieID;

            UpdateAt = DateTime.Now;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(this.Name,
                new ValidationContext(this, null, null) { MemberName = "Genre name" },
                results);

            return results;
        }

    }
}
