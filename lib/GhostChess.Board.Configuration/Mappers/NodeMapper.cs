using GhostChess.Board.Abstractions;
using GhostChess.Board.Core.Configuration;
using GhostChess.Board.Core.Models;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Configuration.Mappers
{
    public class NodeMapper
    {
        private readonly BoardConfiguration _boardConfiguration;

        public NodeMapper(BoardConfiguration boardConfiguration)
        {
            _boardConfiguration = boardConfiguration;
        }

        //TODO: v2 reinforce code to be independent of field sizes
        public Nodes Map()
        {
            var nodes = new Nodes(_boardConfiguration);
            MapCentralBoardNodes(nodes);
            MapCentralIntermediateNodes(nodes);
            MapLeftBoardNodes(nodes);
            MapLeftIntermediateNodes(nodes);
            MapRightBoardNodes(nodes);
            MapRightIntermediateNodes(nodes);
            return nodes;
        }

        private void MapCentralIntermediateNodes(Nodes nodes)
        {
            for(int x = 0; x <= 8; x++)
            {
                for(int y = 0; y <= 8; y++)
                {
                    var letter = (Enums.IntermediateNodeLetters)x;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(letter);
                    sb.Append(y);
                    Node node = new Node(sb.ToString(),
                        _boardConfiguration.CentralBoardZeroX + (x * _boardConfiguration.FieldSizeX), 
                        _boardConfiguration.CentralBoardZeroY + (y * _boardConfiguration.FieldSizeY));
                    nodes.Add(node);
                }
            }
        }

        private void MapLeftIntermediateNodes(Nodes nodes)
        {
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 8; y++)
                {
                    var letter = (Enums.IntermediateNodeLetters)x;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("L");
                    sb.Append(letter);
                    sb.Append(y);
                    Node node = new Node(sb.ToString(),
                        _boardConfiguration.LeftBoardZeroX + (x * _boardConfiguration.FieldSizeX),
                        _boardConfiguration.LeftBoardZeroY + (y * _boardConfiguration.FieldSizeY));
                    nodes.Add(node);
                }
            }
        }

        private void MapRightIntermediateNodes(Nodes nodes)
        {
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 8; y++)
                {
                    var letter = (Enums.IntermediateNodeLetters)x;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("R");
                    sb.Append(letter);
                    sb.Append(y);
                    Node node = new Node(sb.ToString(),
                        _boardConfiguration.RightBoardZeroX + (x * _boardConfiguration.FieldSizeX),
                        _boardConfiguration.RightBoardZeroY + (y * _boardConfiguration.FieldSizeY));
                    nodes.Add(node);
                }
            }
        }

        private void MapCentralBoardNodes(Nodes nodes)
        {
            for(int x = 0; x <= 7; x++)
            {
                for(int y = 0; y <= 7; y++)
                {
                    var letter = (Enums.BoardNodeLetters)(x + 1);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(letter);
                    sb.Append(y + 1);
                    Node node = new Node(sb.ToString(),
                        _boardConfiguration.CentralBoardZeroX + (x * _boardConfiguration.FieldSizeX) + _boardConfiguration.FieldSizeX / 2.0,
                        _boardConfiguration.CentralBoardZeroY + (y * _boardConfiguration.FieldSizeY) + _boardConfiguration.FieldSizeY / 2.0);
                    nodes.Add(node);
                }
            }
        }

        private void MapLeftBoardNodes(Nodes nodes)
        {
            for (int x = 0; x <= 1; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    var letter = (Enums.BoardNodeLetters)(x + 1);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("L");
                    sb.Append(letter);
                    sb.Append(y + 1);
                    Node node = new Node(sb.ToString(),
                        _boardConfiguration.LeftBoardZeroX + (x * _boardConfiguration.FieldSizeX) + _boardConfiguration.FieldSizeX / 2.0,
                        _boardConfiguration.LeftBoardZeroY + (y * _boardConfiguration.FieldSizeY) + _boardConfiguration.FieldSizeY / 2.0);
                    nodes.Add(node);
                }
            }
        }

        private void MapRightBoardNodes(Nodes nodes)
        {
            for (int x = 0; x <= 1; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    var letter = (Enums.BoardNodeLetters)(x + 1);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("R");
                    sb.Append(letter);
                    sb.Append(y + 1);
                    Node node = new Node(sb.ToString(),
                        _boardConfiguration.RightBoardZeroX + (x * _boardConfiguration.FieldSizeX) + _boardConfiguration.FieldSizeX / 2.0,
                        _boardConfiguration.RightBoardZeroY + (y * _boardConfiguration.FieldSizeY) + _boardConfiguration.FieldSizeY / 2.0);
                    nodes.Add(node);
                }
            }
        }
    }
}
