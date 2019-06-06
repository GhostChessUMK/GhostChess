using GhostChess.Board.Abstractions;
using GhostChess.Board.Abstractions.Configuration;
using GhostChess.Board.Abstractions.Controller;
using GhostChess.Board.Abstractions.Pathfinders;
using GhostChess.Board.Core.Models;
using GhostChess.RaspberryPi;
using Microsoft.AspNetCore.SignalR.Client;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GhostChess.Board.Abstractions.Enums;

namespace GhostChess.Board
{
    public class GameHandler
    {
        private readonly IController _controller;
        private readonly IPathfinder _pathfinder;
        private readonly IConfigurationManager _configurationManager;
        private readonly IEnumerable<Node> _nodes;
        private readonly SerialPortStream _serial;
        private readonly HubConnection _connection;
        private readonly Gpio _gpio;
        private Node currentNode;

        public GameHandler(IEnumerable<Node> nodes, Gpio gpio, SerialPortStream serial, IController controller, 
            IPathfinder pathfinder, IConfigurationManager configurationManager, HubConnection connection)
        {
            _controller = controller;
            _pathfinder = pathfinder;
            _configurationManager = configurationManager;
            _connection = connection;
            _nodes = nodes;
            _serial = serial;
            _gpio = gpio;
        }

        public async Task Run()
        {
            await SerialOpen();            
            MoveToZero();
            var color = Colors.White;
            
            _connection.On<string, string>("Move", (source, destination) =>
            {
                Logger.Log("New move!");     
                var sourceNode = GetNodeByName(source);
                var destinationNode = GetNodeByName(destination);

                if (destinationNode.IsEmpty == false)
                {
                    RemovePiece(destinationNode, color);
                }
                MovePiece(sourceNode, destinationNode);
                color = InvertColor(color);
            });

            await _connection.StartAsync();
            Logger.Log("Awaiting players...");
            await Task.Delay(-1);
        }

        private void MovePiece(Node source, Node destination)
        {
            _controller.Move(currentNode, source).Sleep(1000).MagnetOn();
            ExecutePath(_pathfinder.FindPath(source, destination));
            _controller.Sleep(1000).MagnetOff();

            destination.IsEmpty = false;
            source.IsEmpty = true;
        }

        private Colors InvertColor(Colors color) => (color == Colors.White) ? Colors.Black : Colors.White;

        private void RemovePiece(Node node, Colors color) => MovePiece(node, GetFreeSideNode(color));
       
        private Node GetNodeByName(string node) => _nodes.First(n => n.Name.Equals(node.ToUpper()));

        private Node GetFreeSideNode(Colors color)
        {
            var nodes = _nodes as Nodes;
            IEnumerable<Node> sideNodes = (color == Colors.White) ? nodes.GetLeftCentralNodes() : nodes.GetRightCentralNodes();
            return sideNodes.FirstOrDefault(n => n.IsEmpty);
        }

        private void ExecutePath(IEnumerable<Node> path)
        {
            for (int i = 1; i < path.Count(); i++)
            {
                _controller.Move(path.ElementAt(i - 1), path.ElementAt(i));
            }
            currentNode = path.Last();
        }

        private void MoveToZero()
        {
            Logger.Log("Moving to zero...");
            _controller.Move(_configurationManager.BoardConfiguration.LeftBoardZeroX, 
                _configurationManager.BoardConfiguration.LeftBoardZeroY)
                .Sleep(1000);
            currentNode = _nodes.FirstOrDefault(n => n.Name.Equals("LI0"));
        }

        private async Task SerialOpen()
        {
            byte[] buffer = new byte[128];          
            if (_serial.IsOpen == false)
            {
                Logger.Log("Opening serial port...");
                _serial.Open();
            }
            Logger.Log("Awaiting handshake...");
            while (true)
            {
                await _serial.ReadAsync(buffer);
                var response = System.Text.Encoding.Default.GetString(buffer);
                if (response.Contains("start"))
                {
                    Logger.Log("Handshake successful...");
                    break;
                }
                await Task.Delay(250);
            }
            _serial.DiscardOutBuffer();
        }
    }
}
