using GhostChess.Board.Abstractions.Commands;
using GhostChess.RaspberryPi;
using System;

namespace GhostChess.Board.Commands
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
            _gpio.SetState(Enums.State.High);
        }
    }
}
