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

        public int Rate { get; protected set; }

        public int Year { get; set; }
        public string BigPhoto { get; set; }
        public string MainPhoto { get; set; }
        //
        public ICollection<RateDto> Rateds { get; set; }

        public ICollection<CreatorDto> Creators{ get; set; }

        public ICollection<GenreDto> Genres { get; set; }


    }
}
