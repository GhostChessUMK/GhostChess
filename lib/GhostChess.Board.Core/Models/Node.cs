using System.Collections.Generic;

namespace GhostChess.Board.Core.Models
{
    public class Node
    {
        public string Name { get; }
        public double X { get; }
        public double Y { get; }

        public bool IsEmpty { get; set; }

        public Node(string name, double x, double y)
        {
            Name = name;
            X = x;
            Y = y;
            IsEmpty = true;
        }

        public IEnumerable<Node> ConnectedNodes { get; set; }
    }
}
