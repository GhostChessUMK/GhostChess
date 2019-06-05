using GhostChess.Board.Abstractions.Commands;
using GhostChess.Board.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GhostChess.Board.Abstractions.Controller
{
    public interface IController
    {
        IController Move(Node source, Node destination);
        IController Move(double x, double y);
        IController Sleep(int miliseconds);
        IController MagnetOn();
        IController MagnetOff();
        Task Run();
    }
}
