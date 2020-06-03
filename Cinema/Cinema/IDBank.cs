﻿using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace Cinema
{
    class IDBank
    {
        //Int is the OrderNumber, SchedulElement is the actual movie moment. seat is the specific seats.
        static Tuple<string, ScheduleElement, List<Tuple<Seat, Tuple<int, int>>>> selectedOrder;
        

        public static string generateUniqueNumber()
        {
            Guid uniqueNumber = Guid.NewGuid();
            Console.WriteLine(uniqueNumber);
            return uniqueNumber.ToString();
        }

        public static void storeOrder(ScheduleElement se, List<Tuple<Seat, Tuple<int, int>>> ls)
        {
            string ID = generateUniqueNumber();
            selectedOrder = new Tuple<string, ScheduleElement, List<Tuple<Seat,Tuple<int,int>>>>(ID,se,ls);
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

        public static void searchOrder()
        {
            Console.Clear();
            JArray orders = JArray.Parse(File.ReadAllText(@".\orders.json"));
            Console.WriteLine("Please enter the ID of the order.");
            string input = Console.ReadLine();
            foreach (var o in orders)
                if (input == o["ID"].ToString())
                {
                    //This massive sprawling block of spaghetti monster code is supposed to turn the JObject into a correct order to process, don't ask me how
                    //I wrote this code at 2 AM, the only thing keeping me awake is 00's punk
                    List<Tuple<Seat, Tuple<int, int>>> seatlist = new List<Tuple<Seat, Tuple<int, int>>>();
                    foreach (var s in o["Seats"])
                        seatlist.Add(new Tuple<Seat, Tuple<int, int>>(s["Seat"].ToObject<Seat>(), new Tuple<int, int>(int.Parse(s["Coords"]["X"].ToString()), int.Parse(s["Coords"]["Y"].ToString()))));
                    JToken jse = o["Movie"];
                    ScheduleElement se = new ScheduleElement(jse["time"].ToString(), jse["movie"].ToObject<Films>(), Program.rooms[int.Parse(jse["room"].ToString())], jse["date"].ToString());
                    selectedOrder = new Tuple<string, ScheduleElement, List<Tuple<Seat, Tuple<int, int>>>>(input, se, seatlist);

                    //Prints the info of the searched order(Room is not correctly working and should display a number not Cinema.Room)
                    Console.WriteLine("\n\nMovie: {0} \nDate: {1} \nTime: {2} \nRoom: {3} Type: {4}",selectedOrder.Item2.movie.Name ,selectedOrder.Item2.date ,selectedOrder.Item2.time, selectedOrder.Item2.room ,selectedOrder.Item2.room.getType());
                    int count = selectedOrder.Item3.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine("Seat: {0} Row: {1} Chair: {2}", (i+1), selectedOrder.Item3[i].Item2.Item2.ToString() , selectedOrder.Item3[i].Item2.Item1.ToString());
                    }
                    Console.WriteLine("\n\nPress enter to continue..");
                    StandardMessages.PressKeyToContinue();

                }
        }


        public void editOrder()
        {
            
        }

        public void deleteOrder()
        {
            
        }
    }
}
