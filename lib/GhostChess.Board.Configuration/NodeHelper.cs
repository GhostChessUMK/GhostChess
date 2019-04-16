using GhostChess.Board.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GhostChess.Board.Configuration
{
    public static class NodeHelper
    {
        //TODO: Move to separate class with extension methods (Intermediate/Central)
        //TODO: Add Regex to searches
        public static IEnumerable<Node> GetRightCentralNodes(IEnumerable<Node> nodes)
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

        public static IEnumerable<Node> GetLeftCentralNodes(IEnumerable<Node> nodes)
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

        public static IEnumerable<Node> GetNodesAround(IEnumerable<Node> nodes, Node source)
        {
            List<Node> neighbouringNodes = new List<Node>();

            neighbouringNodes.Add(GetLeftNode(nodes, source));
            neighbouringNodes.Add(GetRightNode(nodes, source));
            neighbouringNodes.Add(GetUpperNode(nodes, source));
            neighbouringNodes.Add(GetLowerNode(nodes, source));
            neighbouringNodes.Add(GetUpperLeftNode(nodes, source));
            neighbouringNodes.Add(GetUpperRightNode(nodes, source));
            neighbouringNodes.Add(GetLowerLeftNode(nodes, source));
            neighbouringNodes.Add(GetLowerRightNode(nodes, source));

            neighbouringNodes.RemoveAll(t => t == null);
            return neighbouringNodes;
        }

        //TODO: Check what it returns (is .ToList(); needed?)
        public static IEnumerable<Node> GetAllCentralNodes(IEnumerable<Node> nodes)
        {
            return nodes.Where(t =>
                t.Name.StartsWith("A") ||
                t.Name.StartsWith("B") ||
                t.Name.StartsWith("C") ||
                t.Name.StartsWith("D") ||
                t.Name.StartsWith("E") ||
                t.Name.StartsWith("F") ||
                t.Name.StartsWith("G") ||
                t.Name.StartsWith("H"));
        }
        public static Node GetLeftNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - Constants.FieldSizeX) && t.Y.Equals(origin.Y));
        }

        public static Node GetRightNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + Constants.FieldSizeX) && t.Y.Equals(origin.Y));
        }

        public static Node GetUpperNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y + Constants.FieldSizeY));
        }

        public static Node GetLowerNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y - Constants.FieldSizeY));
        }

        public static Node GetUpperLeftNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (Constants.FieldSizeY / 2.0)));
        }

        public static Node GetLowerLeftNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (Constants.FieldSizeY / 2.0)));
        }
        public static Node GetUpperRightNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (Constants.FieldSizeY / 2.0)));
        }

        public static Node GetLowerRightNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (Constants.FieldSizeY / 2.0)));
        }

        public static IEnumerable<Node> GetLeftIntermediateBoundryNodes(IEnumerable<Node> nodes)
        {
            return nodes.Where(t => t.Name.StartsWith("LK") || t.Name.StartsWith("R"));
        }

        public static IEnumerable<Node> GetRightIntermediateBoundryNodes(IEnumerable<Node> nodes)
        {
            return nodes.Where(t => t.Name.StartsWith("I") || t.Name.StartsWith("RI"));
        }

        public static IEnumerable<Node> GetLeftCentralBoundryNodes(IEnumerable<Node> nodes)
        {
            return nodes.Where(t => t.Name.StartsWith("LB") || t.Name.StartsWith("H"));
        }

        public static IEnumerable<Node> GetRightCentralBoundryNodes(IEnumerable<Node> nodes)
        {
            return nodes.Where(t => t.Name.StartsWith("A") || t.Name.StartsWith("RA"));
        }

        public static Node GetLeftCentralBoundryNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - Constants.FieldSizeX - Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public static Node GetRightCentralBoundryNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + Constants.FieldSizeX + Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public static Node GetLeftIntermediateBoundryNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public static Node GetRightIntermediateBoundryNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public static IEnumerable<Node> GetSetUpNodes(IEnumerable<Node> nodes)
        {
            Regex regex = new Regex(@"[A-H]([1-2]|[7-8])");
            return nodes.Where(t => regex.Match(t.Name).Success);
        }

        public static Node GetInitNode(IEnumerable<Node> nodes)
        {
            return nodes.FirstOrDefault(t => t.Name.Equals("LI0"));
        }
    }
}
