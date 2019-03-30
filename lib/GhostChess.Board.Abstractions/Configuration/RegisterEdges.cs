using GhostChess.Board.Abstractions.Helpers;
using GhostChess.Board.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostChess.Board.Abstractions.Configuration
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
            List<Node> centralNodes = NodeHelper.GetAllCentralNodes(nodes);

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

        private void RegisterCentralAndIntermediateEdges(List<Node> nodes)
        {
            foreach(var node in nodes)
            {
                if(node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes = NodeHelper.GetNodesAround(nodes, node);
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }
        }

        private void RegisterBoundryEdges(List<Node> nodes)
        {
            var leftCentralBoundryNodes = NodeHelper.GetLeftCentralBoundryNodes(nodes);
            var rightCentralBoundryNodes = NodeHelper.GetRightCentralBoundryNodes(nodes);
            var leftIntermediateBoundryNodes = NodeHelper.GetLeftIntermediateBoundryNodes(nodes);
            var rightIntermediateBoundryNodes = NodeHelper.GetRightIntermediateBoundryNodes(nodes);

            foreach(var node in leftCentralBoundryNodes)
            {
                if (node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes.Add(NodeHelper.GetRightCentralBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }

            foreach (var node in rightCentralBoundryNodes)
            {
                if (node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes.Add(NodeHelper.GetLeftCentralBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }

            foreach (var node in leftIntermediateBoundryNodes)
            {
                if (node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes.Add(NodeHelper.GetRightIntermediateBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }

            foreach (var node in rightIntermediateBoundryNodes)
            {
                if (node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes.Add(NodeHelper.GetLeftIntermediateBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }
        }        
    }
}
