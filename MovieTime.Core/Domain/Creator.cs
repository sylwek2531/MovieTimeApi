using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Creator : Entity, IValidatableObject
    {

        public Guid MovieID { get; protected set; }

        [Required]
        [MinLength(90)]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(this.MovieID,
                new ValidationContext(this, null, null) { MemberName = "MovieID" },
                results);
            Validator.TryValidateProperty(this.Name,
                new ValidationContext(this, null, null) { MemberName = "Name" },
                results);

            /*   // some other random test
               if (this.Prop1 > this.Prop2)
               {
                   results.Add(new ValidationResult("Prop1 must be larger than Prop2"));
               }*/

            return results;
        }
    }
}
