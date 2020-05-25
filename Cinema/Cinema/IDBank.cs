using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace Cinema
{
    class IDBank
    {
        //Int is the OrderNumber, SchedulElement is the actual movie moment. seat is the specific seats.
        static Tuple<int, ScheduleElement, List<Tuple<Seat, Tuple<int, int>>>> selectedOrder;
        

        public static int generateUniqueNumber()
        {
            Guid uniqueNumber = Guid.NewGuid();
            Console.WriteLine(uniqueNumber);
            return -1;
        }

        public static void storeOrder(ScheduleElement se, List<Tuple<Seat, Tuple<int, int>>> ls)
        {
            int ID = generateUniqueNumber();
            selectedOrder = new Tuple<int, ScheduleElement, List<Tuple<Seat,Tuple<int,int>>>>(ID,se,ls);
            JObject order = new JObject();
            order["ID"] = ID;
            JObject movie = (JObject)JToken.FromObject(se);
            movie["room"] = Program.rooms.IndexOf(se.room);
            order["Movie"] = movie;
            JArray positons = new JArray();
            
            foreach (var v in ls)
            {
                JObject seat = new JObject();
                seat["Seat"] = (JObject)JToken.FromObject(v.Item1);
                JObject coords = new JObject();
                coords["X"] = v.Item2.Item1;
                coords["Y"] = v.Item2.Item2;
                seat["Coords"] = coords;
                positons.Add(seat);
            }
            
            order["Seats"] = positons;

            string file = File.ReadAllText(@".\orders.json");
            file = file.Remove(file.Length - 3);
            file += "," + order.ToString() + "\n]\n";
            File.WriteAllText(@".\orders.json", file);
            //LET OP DAT DE COORD IN HET JSON MET -1 te weinig wordt gerekend. DUS DAAR MOET REKENING MEE GEHOUDEN WORDEN.
        }

        public void searchOrder()
        {
            
        }

        public void editOrder()
        {
            
        }

        public void deleteOrder()
        {
            
        }
    }
}
