using System;
using System.Collections.Generic;
using System.Text;
using Cinema;

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
    }
    class IDBank: IIDBank
    {
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
