using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.RaspberryPi
{
    public static class Enums
    {
        public enum State
        {
            Low,
            High
        }

        public enum InputType
        {
            Output, 
            Input
        }

        public enum Pins
        {
            Gpio14 = 14,
            Gpio15 = 15,
            Gpio18 = 18,
            Gpio23 = 23,
            Gpio24 = 24,
            Gpio25 = 25,
            Gpio8 = 8,
            Gpio7 = 7,
            Gpio12 = 12,
            Gpio16 = 16,
            Gpio20 = 20,
            Gpio21 = 21,
            Gpio2 = 2,
            Gpio3 = 3,
            Gpio4 = 4,
            Gpio17 = 17,
            Gpio27 = 27,
            Gpio22 = 22,
            Gpio10 = 10,
            Gpio9 = 9,
            Gpio11 = 11,
            Gpio5 = 5,
            Gpio6 = 6,
            Gpio13 = 13,
            Gpio19 = 19,
            Gpio26 = 26
        }
    }
}
