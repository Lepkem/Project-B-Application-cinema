using System;

namespace Cinema
{
   
    class IDBank
    {

        Tuple<int, ScheduleElement, Seat> selectedOrder;



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
    }
}
