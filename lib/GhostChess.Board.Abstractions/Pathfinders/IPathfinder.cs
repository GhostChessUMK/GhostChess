using GhostChess.Board.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Abstractions.Pathfinders
{
    public interface IPathfinder
    {
        IEnumerable<Node> FindPath(Node source, Node target);
    }
}
