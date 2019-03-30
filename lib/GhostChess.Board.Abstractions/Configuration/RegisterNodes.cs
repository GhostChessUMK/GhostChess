using GhostChess.Board.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Abstractions.Configuration
{
    public class RegisterNodes
    {
        public List<Node> Register()
        {
            List<Node> nodes = new List<Node>();
            RegisterCentralBoardNodes(nodes);
            RegisterCentralIntermediateNodes(nodes);
            RegisterLeftBoardNodes(nodes);
            RegisterLeftIntermediateNodes(nodes);
            RegisterRightBoardNodes(nodes);
            RegisterRightIntermediateNodes(nodes);
            return nodes;
        }

        private void RegisterCentralIntermediateNodes(List<Node> nodes)
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
                        Constants.CentralBoardZeroX + (x * Constants.FieldSizeX), 
                        Constants.CentralBoardZeroY + (y * Constants.FieldSizeY));
                    nodes.Add(node);
                }
            }
        }

        private void RegisterLeftIntermediateNodes(List<Node> nodes)
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
                        Constants.LeftBoardZeroX + (x * Constants.FieldSizeX),
                        Constants.LeftBoardZeroY + (y * Constants.FieldSizeY));
                    nodes.Add(node);
                }
            }
        }

        private void RegisterRightIntermediateNodes(List<Node> nodes)
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
                        Constants.RightBoardZeroX + (x * Constants.FieldSizeX),
                        Constants.RightBoardZeroY + (y * Constants.FieldSizeY));
                    nodes.Add(node);
                }
            }
        }

        private void RegisterCentralBoardNodes(List<Node> nodes)
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
                        Constants.CentralBoardZeroX + (x * Constants.FieldSizeX) + Constants.FieldSizeX / 2.0,
                        Constants.CentralBoardZeroY + (y * Constants.FieldSizeY) + Constants.FieldSizeY / 2.0);
                    nodes.Add(node);
                }
            }
        }

        private void RegisterLeftBoardNodes(List<Node> nodes)
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
                        Constants.LeftBoardZeroX + (x * Constants.FieldSizeX) + Constants.FieldSizeX / 2.0,
                        Constants.LeftBoardZeroY + (y * Constants.FieldSizeY) + Constants.FieldSizeY / 2.0);
                    nodes.Add(node);
                }
            }
        }

        private void RegisterRightBoardNodes(List<Node> nodes)
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
                        Constants.RightBoardZeroX + (x * Constants.FieldSizeX) + Constants.FieldSizeX / 2.0,
                        Constants.RightBoardZeroY + (y * Constants.FieldSizeY) + Constants.FieldSizeY / 2.0);
                    nodes.Add(node);
                }
            }
        }
    }
}
