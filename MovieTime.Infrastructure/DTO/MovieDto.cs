using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.DTO
{
    public class MovieDto
    {
        public Guid ID { get; set; }

        public Guid UserID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

    }
}
