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


            File.AppendAllText(@".\orders\orders.json", order.ToString() + Environment.NewLine);
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
