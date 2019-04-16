using GhostChess.Board.Models;
using System.Collections.Generic;
using System.Linq;

namespace GhostChess.Board.Configuration.Mappers
{
    public class EdgeMapper
    {
        private readonly BoardConfiguration _boardConfiguration;
        private readonly NodeHelper _nodeHelper;

        public EdgeMapper(BoardConfiguration boardConfiguration, NodeHelper nodeHelper)
        {
            _boardConfiguration = boardConfiguration;
            _nodeHelper = nodeHelper;
        }

        //TODO: v2 reinforce code to be independent of field sizes
        public void Map(IEnumerable<Node> nodes)
        {
            MapCentralAndIntermediateEdges(nodes);
            MapAdditionalCentralEdges(nodes);
            MapBoundryEdges(nodes);
        }

        private void MapAdditionalCentralEdges(IEnumerable<Node> nodes)
        {
            IEnumerable<Node> centralNodes = _nodeHelper.GetAllCentralNodes(nodes);

            foreach(var node in centralNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(nodes.FirstOrDefault(t => 
                    t.X.Equals(node.X - (_boardConfiguration.FieldSizeX)) && 
                    t.Y.Equals(node.Y + (_boardConfiguration.FieldSizeY))));

                connectedNodes.Add(nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X + (_boardConfiguration.FieldSizeX)) &&
                    t.Y.Equals(node.Y + (_boardConfiguration.FieldSizeY))));

                connectedNodes.Add(nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X - (_boardConfiguration.FieldSizeX)) &&
                    t.Y.Equals(node.Y - (_boardConfiguration.FieldSizeY))));

                connectedNodes.Add(nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X + (_boardConfiguration.FieldSizeX)) &&
                    t.Y.Equals(node.Y - (_boardConfiguration.FieldSizeY))));

                connectedNodes.RemoveAll(t => t == null);
                node.ConnectedNodes = connectedNodes.Distinct().ToList();
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

                node.ConnectedNodes = _nodeHelper.GetNodesAround(nodes, node);
                node.ConnectedNodes = node.ConnectedNodes.Distinct().ToList();
            }
        }

        private void MapBoundryEdges(IEnumerable<Node> nodes)
        {
            var leftCentralBoundryNodes = _nodeHelper.GetLeftCentralBoundryNodes(nodes);
            var rightCentralBoundryNodes = _nodeHelper.GetRightCentralBoundryNodes(nodes);
            var leftIntermediateBoundryNodes = _nodeHelper.GetLeftIntermediateBoundryNodes(nodes);
            var rightIntermediateBoundryNodes = _nodeHelper.GetRightIntermediateBoundryNodes(nodes);

            foreach(var node in leftCentralBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(_nodeHelper.GetRightCentralBoundryNode(nodes, node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in rightCentralBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(_nodeHelper.GetLeftCentralBoundryNode(nodes, node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in leftIntermediateBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(_nodeHelper.GetRightIntermediateBoundryNode(nodes, node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in rightIntermediateBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(_nodeHelper.GetLeftIntermediateBoundryNode(nodes, node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }
        }        
    }
}
