using MovieTime.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Infrastructure.Helpers
{
    public class ValidationHelper
    {
        public Entity _model { get; set; }
        public string _validationMessage { get; set; }

        public ValidationHelper(Entity entity, string validationMessage)
        {
            this._model = entity;
            this._validationMessage = validationMessage;
        }
        public ValidationHelper(string validationMessage)
        {
            this._validationMessage = validationMessage;
        }

        public bool ValidationModel()
        {
            bool validateAllProperties = false;

            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(
                _model,
                new System.ComponentModel.DataAnnotations.ValidationContext(_model, null, null),
                results,
                validateAllProperties);

            if (!isValid)
            {
                throw new MovieTimeException(results, _validationMessage);
            }
            else
            {
                return isValid;
            }
        }
    }
}
