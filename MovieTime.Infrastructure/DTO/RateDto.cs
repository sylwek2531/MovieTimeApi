using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.DTO
{
    public class RateDto
    {

        public Guid ID { get; set; }
        public Guid MovieID { get; set; }

        public Guid UserID { get; set; }

        public int Value { get; set; }




    }
}
