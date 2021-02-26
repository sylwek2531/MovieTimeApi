using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.DTO
{
    public class SearchOptionsDTO
    {
        public string Title { get; set; }
        public int Limit { get; set; }
        public string Creator { get; set; }
        public string Genre{ get; set; }
        public int Popular { get; set; }
    }
}
