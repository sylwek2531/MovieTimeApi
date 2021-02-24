using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Movie : Entity, IValidatableObject
    {

        public Guid UserID { get; protected set; }
        [Required]
        [MinLength(5)]
        public string Title { get; protected set; }
        [Required]
        public string Description { get; protected set; }
        public int Rate { get; protected set; }
        [Required]
        [Range(0, 2020, ErrorMessage = "Year must be a positive number and no more than 2020")]
        public int Year { get; protected set; }

        public virtual User Users { get; set; }

        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Rated> Rateds { get; set; }
        public virtual ICollection<Creator> Creators { get; set; }
        public virtual ICollection<Genre> Genres{ get; set; }

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
            Rate = rate;

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(this.Title,
                new ValidationContext(this, null, null) { MemberName = "Title" },
                results);
            Validator.TryValidateProperty(this.Description,
                new ValidationContext(this, null, null) { MemberName = "Description" },
                results);
            Validator.TryValidateProperty(this.Year,
              new ValidationContext(this, null, null) { MemberName = "Year" },
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
