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
        static Tuple<string, string, ScheduleElement, List<Tuple<Seat, Tuple<int, int>>>> selectedOrder;

        public static string EmailAddressOrder { get; set; }

        public static string generateUniqueNumber()
        {
            Guid uniqueNumber = Guid.NewGuid();
            Console.Clear();
            Console.WriteLine("You succesfully finished ordering your ticket(s).");
            Console.WriteLine("Your order id: " + uniqueNumber + ". Please make sure to save it.\n");
            StandardMessages.PressKeyToContinue();
            return uniqueNumber.ToString();
        }

        public static void storeOrder(ScheduleElement se, List<Tuple<Seat, Tuple<int, int>>> ls)
        {
            string ID = generateUniqueNumber();
            EmailAddressOrder = StandardMessages.GetInputForParam("valid email address, where we will send the order ID to");
            StandardMessages.PressAnyKey();
            StandardMessages.PressKeyToContinue();
            Console.Clear();
            selectedOrder = new Tuple<string, string, ScheduleElement, List<Tuple<Seat,Tuple<int,int>>>>(ID, EmailAddressOrder,se,ls);
            JObject order = new JObject();
            order["ID"] = ID;
            order["EmailAddress"] = EmailAddressOrder;
            JObject movie = (JObject)JToken.FromObject(se);
            movie["room"] = se.room.roomNumber;
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

        /// <summary>
        /// Prints a list of the wanted information beloging to an order of which the ID matches
        /// </summary>
        public static void searchOrderByID()
        {
            Console.Clear();
            JArray orders = JArray.Parse(File.ReadAllText(@".\orders.json"));
            
            string input = StandardMessages.GetInputForParam("ID of the order");
            foreach (var o in orders)
                if (input == o["ID"].ToString())
                {
                    //This massive sprawling block of spaghetti monster code is supposed to turn the JObject into a correct order to process, don't ask me how
                    //I wrote this code at 2 AM, the only thing keeping me awake is 00's punk
                    List<Tuple<Seat, Tuple<int, int>>> seatlist = new List<Tuple<Seat, Tuple<int, int>>>();
                    foreach (var s in o["Seats"])
                        seatlist.Add(new Tuple<Seat, Tuple<int, int>>(s["Seat"].ToObject<Seat>(), new Tuple<int, int>(int.Parse(s["Coords"]["X"].ToString()), int.Parse(s["Coords"]["Y"].ToString()))));
                    JToken jse = o["Movie"];


                    string de1 = jse["time"].ToString();
                    var de2 = jse["movie"].ToObject<Films>();
                    int de3 = int.Parse((string)jse["room"]);
                    var de4 = jse["date"].ToString();

                    if (de3 < 0)
                    {
                        Console.WriteLine("*** Default room selected ***");
                        de3 = 0;
                    }


                    ScheduleElement se = new ScheduleElement(de1, de2, Program.rooms[de3], de4);
                    selectedOrder = new Tuple<string, string, ScheduleElement, List<Tuple<Seat, Tuple<int, int>>>>(input, EmailAddressOrder, se, seatlist);

                    //Prints the info of the searched order(Room is not correctly working and should display a number not Cinema.Room)
                    Console.WriteLine($"\n\nMovie: {selectedOrder.Item3.movie.Name} \nDate: {selectedOrder.Item3.date} \nTime: {selectedOrder.Item3.time} \nRoom: {selectedOrder.Item3.room.roomNumber} Type: {selectedOrder.Item3.room.getType()}");
                    int count = selectedOrder.Item4.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Seat: {(i+1)} Row: {selectedOrder.Item4[i].Item2.Item2.ToString()} Chair: {selectedOrder.Item4[i].Item2.Item1.ToString()}");
                    }
                    Console.WriteLine("\n\nPress enter to continue..");
                    StandardMessages.PressKeyToContinue();

                }
        }

        /// <summary>
        /// Prints a list of the wanted information beloging to an order of which the email matches.
        /// </summary>
        public static void SearchOrderByEmail()
        {
            Console.Clear();
            JArray orders = JArray.Parse(File.ReadAllText(@".\orders.json"));

            string input = StandardMessages.GetInputForParam("Email address of the order");
            foreach (var o in orders)
                if (input == o["EmailAddress"].ToString())
                {
                    //This massive sprawling block of spaghetti monster code is supposed to turn the JObject into a correct order to process, don't ask me how
                    //I wrote this code at 2 AM, the only thing keeping me awake is 00's punk
                    List<Tuple<Seat, Tuple<int, int>>> seatlist = new List<Tuple<Seat, Tuple<int, int>>>();
                    foreach (var s in o["Seats"])
                        seatlist.Add(new Tuple<Seat, Tuple<int, int>>(s["Seat"].ToObject<Seat>(), new Tuple<int, int>(int.Parse(s["Coords"]["X"].ToString()), int.Parse(s["Coords"]["Y"].ToString()))));
                    JToken jse = o["Movie"];


                    string de1 = jse["time"].ToString();
                    var de2 = jse["movie"].ToObject<Films>();
                    int de3 = int.Parse((string)jse["room"]);
                    var de4 = jse["date"].ToString();

                    if (de3 < 0)
                    {
                        Console.WriteLine("*** Default room selected ***");
                        de3 = 0;
                    }


                    ScheduleElement se  = new ScheduleElement(de1, de2, Program.rooms[de3], de4);
                    selectedOrder = new Tuple<string, string, ScheduleElement, List<Tuple<Seat, Tuple<int, int>>>>(input, EmailAddressOrder, se, seatlist);

                    //Prints the info of the searched order(Room is not correctly working and should display a number not Cinema.Room)
                    Console.WriteLine($"\n\nMovie: {selectedOrder.Item3.movie.Name} \nDate: {selectedOrder.Item3.date} \nTime: {selectedOrder.Item3.time} \nRoom: {selectedOrder.Item3.room.roomNumber} Type: {selectedOrder.Item3.room.getType()}");
                    int count = selectedOrder.Item4.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Seat: {(i + 1)} Row: {selectedOrder.Item4[i].Item2.Item2.ToString()} Chair: {selectedOrder.Item4[i].Item2.Item1.ToString()}");
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
