using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Cinema
{
    class ScheduleElement
    {
        public string time { get; set; }
        public Films movie { get; set; }
        public Room room { get; set; }
        public string date { get; set; }

        public ScheduleElement(string t, Films m, Room r, string d)
        {
            time = t;
            movie = m;
            room = r;
            date = d;
        }

        public void printScheduleElement()
        {
           Console.WriteLine(string.Format("{0}, {1} in Maasvlakte {2}\nMovie: {3}\nRuntime: {4}\nGenre: {5}\nSynopsis: {6}\n", date, time,  Program.rooms.IndexOf(room), movie.Name, movie.Runtime, movie.Genre, movie.Synopsis));
        }
    }
}
