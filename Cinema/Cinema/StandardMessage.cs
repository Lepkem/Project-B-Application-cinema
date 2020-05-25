using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema
{
    using Console = System.Console;

    class StandardMessages
    {
        /// <summary>
        /// 
        /// </summary>
        public static void EnterNumber()
        {
            Console.WriteLine($"Please only enter a number!");
        }

        /// <summary>
        /// 
        /// </summary>
        public static void GivenOptions()
        {
            Console.WriteLine($"Only choose from the given options.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionaladdition"></param>
        public static void SomethingWW(string optionaladdition="")
        {
            Console.WriteLine($"Oops! Something went wrong. {optionaladdition}");
        }

        /// <summary>
        /// 
        /// </summary>
        public static void PressAnyKey()
        {
            Console.WriteLine($"Press any key to continue.");
        }
    }
}
