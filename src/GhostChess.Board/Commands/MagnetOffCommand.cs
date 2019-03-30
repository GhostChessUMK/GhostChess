using GhostChess.Board.Abstractions.Commands;
using GhostChess.RaspberryPi;
using System;

namespace GhostChess.Board.Commands
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
            _gpio.SetState(Enums.State.Low);
        }
    }
}
