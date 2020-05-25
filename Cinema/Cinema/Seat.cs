using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema
{
    

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
