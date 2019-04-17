using GhostChess.Board.Core.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GhostChess.Board.Configuration
{
    public class NodeHelper
    {
        private readonly BoardConfiguration _boardConfiguration;

        public NodeHelper(BoardConfiguration boardConfiguration)
        {
            _boardConfiguration = boardConfiguration;
        }

        //TODO: Move to separate class with extension methods (Intermediate/Central)
        //TODO: Add Regex to searches
        //public IEnumerable<Node> GetRightCentralNodes(IEnumerable<Node> nodes)
        //{
        //    List<Node> rightNodes = new List<Node>();
        //    foreach (var node in nodes)
        //    {
        //        if (node.Name.StartsWith("RA") || node.Name.StartsWith("RB"))
        //        {
        //            rightNodes.Add(node);
        //        }
        //    }
        //    return rightNodes;
        //}

        //public  IEnumerable<Node> GetLeftCentralNodes(IEnumerable<Node> nodes)
        //{
        //    List<Node> leftNodes = new List<Node>();
        //    foreach (var node in nodes)
        //    {
        //        if (node.Name.StartsWith("LA") || node.Name.StartsWith("LB"))
        //        {
        //            leftNodes.Add(node);
        //        }
        //    }
        //    return leftNodes;
        //}

        //public  IEnumerable<Node> GetNodesAround(IEnumerable<Node> nodes, Node source)
        //{
        //    List<Node> neighbouringNodes = new List<Node>();

        //    neighbouringNodes.Add(GetLeftNode(nodes, source));
        //    neighbouringNodes.Add(GetRightNode(nodes, source));
        //    neighbouringNodes.Add(GetUpperNode(nodes, source));
        //    neighbouringNodes.Add(GetLowerNode(nodes, source));
        //    neighbouringNodes.Add(GetUpperLeftNode(nodes, source));
        //    neighbouringNodes.Add(GetUpperRightNode(nodes, source));
        //    neighbouringNodes.Add(GetLowerLeftNode(nodes, source));
        //    neighbouringNodes.Add(GetLowerRightNode(nodes, source));

        //    neighbouringNodes.RemoveAll(t => t == null);
        //    return neighbouringNodes;
        //}

        //public  IEnumerable<Node> GetAllCentralNodes(IEnumerable<Node> nodes)
        //{
        //    return nodes.Where(t =>
        //        t.Name.StartsWith("A") ||
        //        t.Name.StartsWith("B") ||
        //        t.Name.StartsWith("C") ||
        //        t.Name.StartsWith("D") ||
        //        t.Name.StartsWith("E") ||
        //        t.Name.StartsWith("F") ||
        //        t.Name.StartsWith("G") ||
        //        t.Name.StartsWith("H"))
        //        .ToList();
        //}

        //public Node GetLeftNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X - _boardConfiguration.FieldSizeX) && t.Y.Equals(origin.Y));
        //}

        //public  Node GetRightNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X + _boardConfiguration.FieldSizeX) && t.Y.Equals(origin.Y));
        //}

        //public  Node GetUpperNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y + _boardConfiguration.FieldSizeY));
        //}

        //public  Node GetLowerNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y - _boardConfiguration.FieldSizeY));
        //}

        //public  Node GetUpperLeftNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (_boardConfiguration.FieldSizeY / 2.0)));
        //}

        //public  Node GetLowerLeftNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (_boardConfiguration.FieldSizeY / 2.0)));
        //}
        //public  Node GetUpperRightNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (_boardConfiguration.FieldSizeY / 2.0)));
        //}

        //public  Node GetLowerRightNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (_boardConfiguration.FieldSizeY / 2.0)));
        //}

        //public  IEnumerable<Node> GetLeftIntermediateBoundryNodes(IEnumerable<Node> nodes)
        //{
        //    return nodes.Where(t => t.Name.StartsWith("LK") || t.Name.StartsWith("R")).ToList();
        //}

        //public  IEnumerable<Node> GetRightIntermediateBoundryNodes(IEnumerable<Node> nodes)
        //{
        //    return nodes.Where(t => t.Name.StartsWith("I") || t.Name.StartsWith("RI")).ToList();
        //}

        //public  IEnumerable<Node> GetLeftCentralBoundryNodes(IEnumerable<Node> nodes)
        //{
        //    return nodes.Where(t => t.Name.StartsWith("LB") || t.Name.StartsWith("H")).ToList();
        //}

        //public  IEnumerable<Node> GetRightCentralBoundryNodes(IEnumerable<Node> nodes)
        //{
        //    return nodes.Where(t => t.Name.StartsWith("A") || t.Name.StartsWith("RA")).ToList();
        //}

        //public  Node GetLeftCentralBoundryNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X - _boardConfiguration.FieldSizeX - _boardConfiguration.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        //}

        //public  Node GetRightCentralBoundryNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X + _boardConfiguration.FieldSizeX + _boardConfiguration.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        //}

        //public  Node GetLeftIntermediateBoundryNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X - _boardConfiguration.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        //}

        //public  Node GetRightIntermediateBoundryNode(IEnumerable<Node> nodes, Node origin)
        //{
        //    return nodes.FirstOrDefault(t => t.X.Equals(origin.X + _boardConfiguration.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        //}

        //public  IEnumerable<Node> GetSetUpNodes(IEnumerable<Node> nodes)
        //{
        //    Regex regex = new Regex(@"[A-H]([1-2]|[7-8])");
        //    return nodes.Where(t => regex.Match(t.Name).Success).ToList();
        //}

        //public  Node GetInitNode(IEnumerable<Node> nodes)
        //{
        //    return nodes.FirstOrDefault(t => t.Name.Equals("LI0"));
        //}
    }
}
