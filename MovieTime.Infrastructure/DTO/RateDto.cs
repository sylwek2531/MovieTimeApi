using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.DTO
{
    public class RateDto
    {

        public Guid MovieID { get; set; }

        public Guid UserID { get; set; }

        public int Value { get; set; }




    }
}
