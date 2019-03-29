using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ChessboardSteering.Commands
{
    public class SleepCommand : ICommand
    {
        private int _miliseconds;

        public SleepCommand(int miliseconds)
        {
            _miliseconds = miliseconds;
        }

        public void Execute()
        {
            Console.WriteLine($"Sleeping for {_miliseconds} ms");
            Thread.Sleep(_miliseconds);
        }
    }
}
