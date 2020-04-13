using System;

namespace Cinema
{
    interface IIDBank
    {
        void generateUniqueNumber();
        void storeOrder();
        void searchOrder();
        void editOrder();
        void deleteOrder();
    }
    class IDBank : IIDBank
    {
        private string selectedOrder { get; set; } //why is this private in the UML?
                                                   //and why is this the type in the UML: [int, ScheduleElement, string]?
        public void generateUniqueNumber()
        {
            Guid uniqueNumber = new Guid();
        }

        public void storeOrder()
        {
            throw new NotImplementedException();
        }

        public void searchOrder()
        {
            throw new NotImplementedException();
        }

        public void editOrder()
        {
            throw new NotImplementedException();
        }

        public void deleteOrder()
        {
            throw new NotImplementedException();
        }
    }
}
