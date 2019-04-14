using GhostChess.Board.Abstractions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GhostChess.Board.Abstractions.Helpers
{
    public static class NodeHelper
    {
        public static List<Node> GetRightCentralNodes(List<Node> nodes)
        {
            List<Node> rightNodes = new List<Node>();
            foreach (var node in nodes)
            {
                if (node.Name.StartsWith("RA") || node.Name.StartsWith("RB"))
                {
                    rightNodes.Add(node);
                }
            }
            return rightNodes;
        }

        public static List<Node> GetLeftCentralNodes(List<Node> nodes)
        {
            List<Node> leftNodes = new List<Node>();
            foreach (var node in nodes)
            {
                if (node.Name.StartsWith("LA") || node.Name.StartsWith("LB"))
                {
                    leftNodes.Add(node);
                }
            }
            return leftNodes;
        }

        public static List<Node> GetNodesAround(List<Node> nodes, Node origin)
        {
            List<Node> neighbouringNodes = new List<Node>();

            neighbouringNodes.Add(GetLeftNode(nodes, origin));
            neighbouringNodes.Add(GetRightNode(nodes, origin));
            neighbouringNodes.Add(GetUpperNode(nodes, origin));
            neighbouringNodes.Add(GetLowerNode(nodes, origin));
            neighbouringNodes.Add(GetUpperLeftNode(nodes, origin));
            neighbouringNodes.Add(GetUpperRightNode(nodes, origin));
            neighbouringNodes.Add(GetLowerLeftNode(nodes, origin));
            neighbouringNodes.Add(GetLowerRightNode(nodes, origin));

            neighbouringNodes.RemoveAll(t => t == null);
            return neighbouringNodes;
        }

        public static List<Node> GetAllCentralNodes(List<Node> nodes)
        {
            return nodes.FindAll(t =>
                t.Name.StartsWith("A") ||
                t.Name.StartsWith("B") ||
                t.Name.StartsWith("C") ||
                t.Name.StartsWith("D") ||
                t.Name.StartsWith("E") ||
                t.Name.StartsWith("F") ||
                t.Name.StartsWith("G") ||
                t.Name.StartsWith("H"));
        }
        public static Node GetLeftNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - Constants.FieldSizeX) && t.Y.Equals(origin.Y));
        }

        public static Node GetRightNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + Constants.FieldSizeX) && t.Y.Equals(origin.Y));
        }

        public static Node GetUpperNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y + Constants.FieldSizeY));
        }

        public static Node GetLowerNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y - Constants.FieldSizeY));
        }

        public static Node GetUpperLeftNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (Constants.FieldSizeY / 2.0)));
        }

        public static Node GetLowerLeftNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (Constants.FieldSizeY / 2.0)));
        }
        public static Node GetUpperRightNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (Constants.FieldSizeY / 2.0)));
        }

        public static Node GetLowerRightNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (Constants.FieldSizeY / 2.0)));
        }

        public static List<Node> GetLeftIntermediateBoundryNodes(List<Node> nodes)
        {
            return nodes.FindAll(t => t.Name.StartsWith("LK") || t.Name.StartsWith("R"));
        }

        public static List<Node> GetRightIntermediateBoundryNodes(List<Node> nodes)
        {
            return nodes.FindAll(t => t.Name.StartsWith("I") || t.Name.StartsWith("RI"));
        }

        public static List<Node> GetLeftCentralBoundryNodes(List<Node> nodes)
        {
            return nodes.FindAll(t => t.Name.StartsWith("LB") || t.Name.StartsWith("H"));
        }

        public static List<Node> GetRightCentralBoundryNodes(List<Node> nodes)
        {
            return nodes.FindAll(t => t.Name.StartsWith("A") || t.Name.StartsWith("RA"));
        }

        public static Node GetLeftCentralBoundryNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - Constants.FieldSizeX - Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public static Node GetRightCentralBoundryNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + Constants.FieldSizeX + Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public static Node GetLeftIntermediateBoundryNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public static Node GetRightIntermediateBoundryNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public static List<Node> GetSetUpNodes(List<Node> nodes)
        {
            Regex regex = new Regex(@"[A-H]([1-2]|[7-8])");
            return nodes.FindAll(t => regex.Match(t.Name).Success);
        }

        public static Node GetInitNode(List<Node> nodes)
        {
            return nodes.FirstOrDefault(t => t.Name.Equals("LI0"));
        }
    }
}
