using GhostChess.Board.Models;
using System.Collections.Generic;
using System.Linq;

namespace GhostChess.Board.Configuration.Mappers
{
    public class EdgeMapper
    {
        //TODO: v2 reinforce code to be independent of field sizes
        public void Map(IEnumerable<Node> nodes)
        {
            MapCentralAndIntermediateEdges(nodes);
            MapAdditionalCentralEdges(nodes);
            MapBoundryEdges(nodes);
        }

        private void MapAdditionalCentralEdges(IEnumerable<Node> nodes)
        {
            IEnumerable<Node> centralNodes = NodeHelper.GetAllCentralNodes(nodes);

            foreach(var node in centralNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(nodes.FirstOrDefault(t => 
                    t.X.Equals(node.X - (Constants.FieldSizeX)) && 
                    t.Y.Equals(node.Y + (Constants.FieldSizeY))));

                connectedNodes.Add(nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X + (Constants.FieldSizeX)) &&
                    t.Y.Equals(node.Y + (Constants.FieldSizeY))));

                connectedNodes.Add(nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X - (Constants.FieldSizeX)) &&
                    t.Y.Equals(node.Y - (Constants.FieldSizeY))));

                connectedNodes.Add(nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X + (Constants.FieldSizeX)) &&
                    t.Y.Equals(node.Y - (Constants.FieldSizeY))));

                connectedNodes.RemoveAll(t => t == null);
                node.ConnectedNodes = connectedNodes.Distinct();
            }

        }

        private void MapCentralAndIntermediateEdges(IEnumerable<Node> nodes)
        {
            foreach(var node in nodes)
            {
                if(node.ConnectedNodes == null)
                {
                    node.ConnectedNodes = new List<Node>();
                }

                node.ConnectedNodes = NodeHelper.GetNodesAround(nodes, node);
                node.ConnectedNodes = node.ConnectedNodes.Distinct();
            }
        }

        private void MapBoundryEdges(IEnumerable<Node> nodes)
        {
            var leftCentralBoundryNodes = NodeHelper.GetLeftCentralBoundryNodes(nodes);
            var rightCentralBoundryNodes = NodeHelper.GetRightCentralBoundryNodes(nodes);
            var leftIntermediateBoundryNodes = NodeHelper.GetLeftIntermediateBoundryNodes(nodes);
            var rightIntermediateBoundryNodes = NodeHelper.GetRightIntermediateBoundryNodes(nodes);

            foreach(var node in leftCentralBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(NodeHelper.GetRightCentralBoundryNode(nodes, node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in rightCentralBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(NodeHelper.GetLeftCentralBoundryNode(nodes, node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in leftIntermediateBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(NodeHelper.GetRightIntermediateBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct();
            }

            foreach (var node in rightIntermediateBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(NodeHelper.GetLeftIntermediateBoundryNode(nodes, node));
                node.ConnectedNodes = node.ConnectedNodes.Distinct();
            }
        }        
    }
}
