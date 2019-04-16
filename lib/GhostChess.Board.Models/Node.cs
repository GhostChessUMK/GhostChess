using System.Collections.Generic;

namespace GhostChess.Board.Models
{
    public class Node
    {
        public string Name { get; }
        public double X { get; }
        public double Y { get; }

        public bool isEmpty { get; set; }

        public Node(string name, double x, double y)
        {
            Name = name;
            X = x;
            Y = y;
            isEmpty = true;
        }

        public IEnumerable<Node> ConnectedNodes { get; set; }
    }
}
