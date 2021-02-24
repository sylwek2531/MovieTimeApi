using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class Rated : Entity, IValidatableObject
    {

        public Guid UserID { get; protected set; }
        public Guid MovieID { get; protected set; }
        [Required]
        [Range(0, 5, ErrorMessage = "Value must be a positive number and no more than 5")]
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
        public void setValue(int value)
        {
        
            Value = value;

            UpdateAt = DateTime.Now;
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(this.Value,
                new ValidationContext(this, null, null) { MemberName = "Value" },
                results);

            return results;
        }

    }
}
