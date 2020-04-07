using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Cinema
{
    class ScheduleElement
    {
        string time;
        string movie;
        Room room;
        string date;
        int roomType;

        public ScheduleElement(string t, string m, Room r, string d)
        {
            time = t;
            movie = m;
            room = r;
            date = d;
        }

        public void printScheduleElement()
        {
            Console.WriteLine(string.Format("Playing {0} at {1} on {2} in Maasvlakte {3}", movie, time, date, Program.rooms.IndexOf(room)));
        }
    }
}
