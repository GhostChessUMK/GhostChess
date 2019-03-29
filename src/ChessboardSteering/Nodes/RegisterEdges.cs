using ChessboardSteering.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessboardSteering.Nodes
{
    public class RegisterEdges
    {
        public void Register(List<Node> nodes)
        {
            RegisterCentralAndIntermediateEdges(nodes);
            RegisterAdditionalCentralEdges(nodes);
            RegisterBoundryEdges(nodes);
        }

        private void RegisterAdditionalCentralEdges(List<Node> nodes)
        {
            List<Node> centralNodes = GetAllCentralNodes(nodes);

            foreach(var node in centralNodes)
            {
                if (node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes.Add(nodes.FirstOrDefault(t => 
                    t.X.Equals(node.X - (Constants.FieldSizeX)) && 
                    t.Y.Equals(node.Y + (Constants.FieldSizeY))));

                node.ConnectedNodes.Add(nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X + (Constants.FieldSizeX)) &&
                    t.Y.Equals(node.Y + (Constants.FieldSizeY))));

                node.ConnectedNodes.Add(nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X - (Constants.FieldSizeX)) &&
                    t.Y.Equals(node.Y - (Constants.FieldSizeY))));

                node.ConnectedNodes.Add(nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X + (Constants.FieldSizeX)) &&
                    t.Y.Equals(node.Y - (Constants.FieldSizeY))));

                node.ConnectedNodes.RemoveAll(t => t == null);
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }

        }

        private List<Node> GetAllCentralNodes(List<Node> nodes)
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

        private void RegisterCentralAndIntermediateEdges(List<Node> nodes)
        {
            foreach(var node in nodes)
            {
                if(node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes = GetNodesAround(nodes, node);
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }
        }

        private void RegisterBoundryEdges(List<Node> nodes)
        {
            var leftCentralBoundryNodes = GetLeftCentralBoundryNodes(nodes);
            var rightCentralBoundryNodes = GetRightCentralBoundryNodes(nodes);
            var leftIntermediateBoundryNodes = GetLeftIntermediateBoundryNodes(nodes);
            var rightIntermediateBoundryNodes = GetRightIntermediateBoundryNodes(nodes);

            foreach(var node in leftCentralBoundryNodes)
            {
                if (node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes.Add(GetRightCentralBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }

            foreach (var node in rightCentralBoundryNodes)
            {
                if (node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes.Add(GetLeftCentralBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }

            foreach (var node in leftIntermediateBoundryNodes)
            {
                if (node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes.Add(GetRightIntermediateBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }

            foreach (var node in rightIntermediateBoundryNodes)
            {
                if (node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes.Add(GetLeftIntermediateBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }
        }

        private List<Node> GetNodesAround(List<Node> nodes, Node origin)
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

        private Node GetLeftNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - Constants.FieldSizeX) && t.Y.Equals(origin.Y));
        }

        private Node GetRightNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + Constants.FieldSizeX) && t.Y.Equals(origin.Y));
        }

        private Node GetUpperNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y + Constants.FieldSizeY));
        }

        private Node GetLowerNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y - Constants.FieldSizeY));
        }

        private Node GetUpperLeftNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (Constants.FieldSizeY / 2.0)));
        }

        private Node GetLowerLeftNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (Constants.FieldSizeY / 2.0)));
        }

        private Node GetUpperRightNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (Constants.FieldSizeY / 2.0)));
        }

        private Node GetLowerRightNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (Constants.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (Constants.FieldSizeY / 2.0)));
        }

        private List<Node> GetLeftIntermediateBoundryNodes(List<Node> nodes)
        {
            return nodes.FindAll(t => t.Name.StartsWith("LK") || t.Name.StartsWith("R"));
        }

        private List<Node> GetRightIntermediateBoundryNodes(List<Node> nodes)
        {
            return nodes.FindAll(t => t.Name.StartsWith("I") || t.Name.StartsWith("RI"));
        }

        private List<Node> GetLeftCentralBoundryNodes(List<Node> nodes)
        {
            return nodes.FindAll(t => t.Name.StartsWith("LB") || t.Name.StartsWith("H"));
        }

        private List<Node> GetRightCentralBoundryNodes(List<Node> nodes)
        {
            return nodes.FindAll(t => t.Name.StartsWith("A") || t.Name.StartsWith("RA"));
        }

        private Node GetLeftCentralBoundryNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - Constants.FieldSizeX - Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        private Node GetRightCentralBoundryNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + Constants.FieldSizeX + Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        private Node GetLeftIntermediateBoundryNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        private Node GetRightIntermediateBoundryNode(List<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + Constants.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }
    }
}
