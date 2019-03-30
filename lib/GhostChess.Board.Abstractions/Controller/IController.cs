using GhostChess.Board.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Abstractions.Controller
{
    public interface IController
    {
        IController Move(Node source, Node destination);
        IController Sleep(int miliseconds);
        IController MagnetOn();
        IController MagnetOff();
        void Execute();
    }
}
