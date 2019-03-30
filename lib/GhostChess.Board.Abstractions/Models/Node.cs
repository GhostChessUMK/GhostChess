using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Abstractions.Models
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

        public List<Node> ConnectedNodes { get; set; }
    }
}
