using GhostChess.Board.Core.Configuration;
using GhostChess.Board.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace GhostChess.Board.Configuration.Mappers
{
    public class EdgeMapper
    {
        private readonly BoardConfiguration _boardConfiguration;

        public EdgeMapper(BoardConfiguration boardConfiguration)
        {
            _boardConfiguration = boardConfiguration;
        }

        //TODO: v2 reinforce code to be independent of field sizes
        public void Map(Nodes nodes)
        {
            MapCentralAndIntermediateEdges(nodes);
            MapAdditionalCentralEdges(nodes);
            MapBoundryEdges(nodes);
        }

        private void MapAdditionalCentralEdges(Nodes nodes)
        {
            var centralNodes = nodes.GetAllCentralNodes();

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

        private void MapCentralAndIntermediateEdges(Nodes nodes)
        {
            foreach(var node in nodes)
            {
                var connectedNodes = node.ConnectedNodes.ToList() ?? new List<Node>();
                connectedNodes.AddRange(nodes.GetNodesAround(node));
                node.ConnectedNodes = connectedNodes.Distinct().ToList();
            }
        }

        private void MapBoundryEdges(Nodes nodes)
        {
            var leftCentralBoundryNodes = nodes.GetLeftCentralBoundryNodes();
            var rightCentralBoundryNodes = nodes.GetRightCentralBoundryNodes();
            var leftIntermediateBoundryNodes = nodes.GetLeftIntermediateBoundryNodes();
            var rightIntermediateBoundryNodes = nodes.GetRightIntermediateBoundryNodes();

            foreach(var node in leftCentralBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(nodes.GetRightCentralBoundryNode(node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in rightCentralBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(nodes.GetLeftCentralBoundryNode(node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in leftIntermediateBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(nodes.GetRightIntermediateBoundryNode(node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }

            foreach (var node in rightIntermediateBoundryNodes)
            {
                List<Node> connectedNodes = new List<Node>();
                if (node.ConnectedNodes != null)
                {
                    connectedNodes = node.ConnectedNodes.ToList();
                }

                connectedNodes.Add(nodes.GetLeftIntermediateBoundryNode(node));
                node.ConnectedNodes = connectedNodes.Distinct();
            }
        }        
    }
}
