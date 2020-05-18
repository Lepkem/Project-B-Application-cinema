using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema
{
    class SeatV2
    {
        public bool vacant;

        public int seatNumber;

        public int rowNumber;

        public int priceCategory;

        public SeatV2()
        {
            vacant = true;
            priceCategory = 0;
        }
    }

    class Seat
    {
        public bool vacant;
        public float priceMod;

        public Seat(bool v, float p)
        {
            vacant = v;
            priceMod = p;
        }
    }
}
