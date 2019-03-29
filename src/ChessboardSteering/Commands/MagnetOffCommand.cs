using GhostChess.RaspberryPi;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessboardSteering.Commands
{
    public class MagnetOffCommand : ICommand
    {
        private Gpio _gpio;

        public MagnetOffCommand(Gpio gpio)
        {
            _gpio = gpio;
        }

        public void Execute()
        {
            Console.WriteLine("Magnet is off");
            //_gpio.SetState(Enums.State.Low);
        }
    }
}
