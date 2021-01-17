using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.DTO
{
    public class MovieDto
    {
        public Guid Id { get; set; }

        public Guid Id_user { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Rate { get; set; }


    }
}
