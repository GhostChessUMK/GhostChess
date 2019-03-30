using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Abstractions.Commands
{
    public interface ICommand
    {
        void Execute();
    }
}
