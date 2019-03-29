using ChessboardSteering.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessboardSteering.Controller
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
