using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieTime.Infrastructure.Helpers
{
    public class MovieTimeException : Exception
    {

        private List<ValidationResult> _data;
        public List<ValidationResult> getData { get { return _data; } }

        public MovieTimeException(List<ValidationResult> data, string Message) : base(Message)
        {
            _data = data;
        }

        public MovieTimeException(string message) : base(message)
        {
        }
    }
}
