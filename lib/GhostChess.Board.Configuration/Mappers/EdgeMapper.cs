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

        private void MapCentralAndIntermediateEdges(IEnumerable<Node> nodes)
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

        private void MapBoundryEdges(IEnumerable<Node> nodes)
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
