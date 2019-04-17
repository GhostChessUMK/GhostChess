using GhostChess.Board.Core.Configuration;
using GhostChess.Board.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace GhostChess.Board.Configuration.Mappers
{
    public class EdgeMapper
    {
        private readonly BoardConfiguration _boardConfiguration;
        private readonly NodeHelper _nodeHelper;
        private readonly Nodes _nodes;

        public EdgeMapper(BoardConfiguration boardConfiguration, NodeHelper nodeHelper, Nodes nodes)
        {
            _boardConfiguration = boardConfiguration;
            _nodeHelper = nodeHelper;
            _nodes = nodes;
        }

        //TODO: v2 reinforce code to be independent of field sizes
        public void Map()
        {
            MapCentralAndIntermediateEdges();
            MapAdditionalCentralEdges();
            MapBoundryEdges();
        }

        private void MapAdditionalCentralEdges()
        {
            var centralNodes = _nodes.GetAllCentralNodes();

            foreach(var node in centralNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(_nodes.FirstOrDefault(t => 
                    t.X.Equals(node.X - (_boardConfiguration.FieldSizeX)) && 
                    t.Y.Equals(node.Y + (_boardConfiguration.FieldSizeY))));

                connectedNodes.Add(_nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X + (_boardConfiguration.FieldSizeX)) &&
                    t.Y.Equals(node.Y + (_boardConfiguration.FieldSizeY))));

                connectedNodes.Add(_nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X - (_boardConfiguration.FieldSizeX)) &&
                    t.Y.Equals(node.Y - (_boardConfiguration.FieldSizeY))));

                connectedNodes.Add(_nodes.FirstOrDefault(t =>
                    t.X.Equals(node.X + (_boardConfiguration.FieldSizeX)) &&
                    t.Y.Equals(node.Y - (_boardConfiguration.FieldSizeY))));

                connectedNodes.RemoveAll(t => t == null);
                node.ConnectedNodes = connectedNodes.Distinct().ToList();
            }

        }

        private void MapCentralAndIntermediateEdges()
        {
            foreach(var node in _nodes)
            {
                var connectedNodes = node.ConnectedNodes.ToList() ?? new List<Node>();
                connectedNodes.AddRange(_nodes.GetNodesAround(node));
                node.ConnectedNodes = connectedNodes.Distinct().ToList();
            }
        }

        private void MapBoundryEdges()
        {
            var leftCentralBoundryNodes = _nodes.GetLeftCentralBoundryNodes();
            var rightCentralBoundryNodes = _nodes.GetRightCentralBoundryNodes();
            var leftIntermediateBoundryNodes = _nodes.GetLeftIntermediateBoundryNodes();
            var rightIntermediateBoundryNodes = _nodes.GetRightIntermediateBoundryNodes();

            foreach(var node in leftCentralBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(_nodes.GetRightCentralBoundryNode(node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in rightCentralBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(_nodes.GetLeftCentralBoundryNode(node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in leftIntermediateBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(_nodes.GetRightIntermediateBoundryNode(node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in rightIntermediateBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(_nodes.GetLeftIntermediateBoundryNode(node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }
        }        
    }
}
