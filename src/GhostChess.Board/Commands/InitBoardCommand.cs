using GhostChess.Board.Abstractions.Commands;
using GhostChess.Board.Abstractions.Helpers;
using GhostChess.Board.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GhostChess.Board.Commands
{
    public class InitBoardCommand : ICommand
    {
        private readonly List<Node> _nodes;

        public InitBoardCommand(List<Node> nodes)
        {
            _nodes = NodeHelper.GetSetUpNodes(nodes);
        }

        public async Task Execute()
        {
            foreach(var node in _nodes)
            {
                node.isEmpty = false;
            }
        }
    }
}
