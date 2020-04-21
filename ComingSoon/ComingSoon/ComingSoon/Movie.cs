using System;
using System.Collections.Generic;
using System.Text;

namespace ComingSoon
{
    public class Movie // here I made a test class
    {
        public string name { get; set; }
        public  string genre { get; set; }
        public uint runtime { get; set; } //uint == only positive values 
        public string synopsis { get; set; }
        public DateTime releasedate { get; set; }   
        public Movie()
        {
            
        }

    }

    public class Whatever
    {
        public List<Movie> data { get; set; }
    }
}
