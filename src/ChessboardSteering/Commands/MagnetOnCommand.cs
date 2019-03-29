using GhostChess.RaspberryPi;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessboardSteering.Commands
{
    public class MagnetOnCommand : ICommand
    {
        private Gpio _gpio;

        public MagnetOnCommand(Gpio gpio)
        {
            _gpio = gpio;
        }

        public void Execute()
        {
            Console.WriteLine("Magnet is on");
            //_gpio.SetState(Enums.State.High);
        }
    }
}
