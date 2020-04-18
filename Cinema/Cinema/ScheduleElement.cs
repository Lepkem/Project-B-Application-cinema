using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Cinema
{
    class ScheduleElement
    {
        string time;
        Films movie;
        Room room;
        string date;

        public ScheduleElement(string t, Films m, Room r, string d)
        {
            time = t;
            movie = m;
            room = r;
            date = d;
        }

        public void printScheduleElement()
        {
            Console.WriteLine(string.Format("Playing {0} at {1} on {2} in Maasvlakte {3}\nRuntime: {4}\nGenre: {5}\nSynopsis: {6}\n", movie.Name, time, date, Program.rooms.IndexOf(room), movie.Runtime, movie.Genre, movie.Synopsis));
        }
    }
}
