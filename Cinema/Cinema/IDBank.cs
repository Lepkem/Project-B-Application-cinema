using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace Cinema
{
    /*
    class Order
    {
        public int number;
        public ScheduleElement scheduleElement;
        public Seat seat;

       
        public Order(int number,ScheduleElement scheduleElement, Seat seat)
        {
            this.number = number;
            this.scheduleElement = scheduleElement;
            this.seat = seat;

            Tuple<int, ScheduleElement, Seat> selectedOrder = new Tuple<int, ScheduleElement, Seat>(number,scheduleElement,seat);

        }

    }
    */
   

    class IDBank
    {
        List<IDBank> orderList;
        private string jsonFileLocation;

        

        public IDBank(string jsonFileLocation)
        {
            //the actual initialization function is its own method so that it can be called manually
            this.jsonFileLocation = jsonFileLocation;
            initialize();
        }

        public void generateUniqueNumber()
        {
            Guid uniqueNumber = Guid.NewGuid();
            Console.WriteLine(uniqueNumber);
        }

        public void storeOrder()
        {

            
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


        public void initialize()
        {
            List<ScheduleElement> schedule = new List<ScheduleElement>();
            schedule = Program.schedule;
            this.orderList = new List<IDBank>();
            JArray orderArray = JArray.Parse(File.ReadAllText(this.jsonFileLocation));
            foreach (JObject obj in orderArray)
            {
            //    this.orderList.Add(new order((int)obj["orderNumber"], (string)obj["ScheduleElement"], (string)obj["seat"]));
            }
        }
    }
}
