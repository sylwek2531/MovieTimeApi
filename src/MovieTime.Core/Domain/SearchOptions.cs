using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Domain
{
    public class SearchOptions
    {
        public string Title { get; set; }
        public int Limit { get; set; }
        public string Creator { get; set; }
        public string Genre { get; set; }
        public int Popular { get; set; }

        public SearchOptions()
        {

        }
        public SearchOptions(string title, int limit, string creator, string genre, int popular)
        {
            Title = title;
            Limit = limit;
            Creator = creator;
            Genre = genre;
            Popular = popular;
        }
    }
}
