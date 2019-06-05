using GhostChess.Board.Abstractions.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GhostChess.Board.Commands
{
    public class SleepCommand : ICommand
    {
        private int _miliseconds;

        public SleepCommand(int miliseconds)
        {
            _miliseconds = miliseconds;
        }

        public async Task Execute()
        {
            Logger.Log($"Sleeping for {_miliseconds} ms");
            Thread.Sleep(_miliseconds);
        }
    }
}
