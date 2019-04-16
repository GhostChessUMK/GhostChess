using GhostChess.Board.Abstractions;
using GhostChess.Board.Abstractions.Configuration;
using GhostChess.Board.Configuration.Mappers;
using GhostChess.Board.Configuration;
using GhostChess.Board.Models;
using GhostChess.RaspberryPi;
using Microsoft.AspNetCore.SignalR.Client;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostChess.Board.Algorithms.Pathfinders;
using GhostChess.Board.Abstractions.Pathfinders;

namespace GhostChess.Board
{
    class Program
    {
        //TODO: Move to gamehandler class, create logger with console.writelines for more information and/or adding to file, (perhaps return stream)
        //TODO: Template pattern (program flow), Initialize, Execute ^ (simmilar)
        //TODO: Clean up SignalR, move parts of code to separate methods
        private const int ERROR_BAD_ARGUMENTS = 0xA0;

        public static async Task Main(string[] args)
        {
            if (args.Count().Equals(0))
            {
                Console.WriteLine("Error! Put serial port name in arguments ex.");
                Console.WriteLine("sudo dotnet GhostChess.Board.dll /dev/ttyUSB0");
                Console.WriteLine("./run.sh /dev/ttyUSB0");
                Environment.Exit(ERROR_BAD_ARGUMENTS);
            }

            Console.WriteLine("Configuring board...");
            var fieldSize = 40;
            var boardZeroX = 60;
            var boardZeroY = 20;
            var sideFieldOffset = 10;
            var boardConfiguration = new BoardConfiguration
            {
                LeftBoardZeroX = boardZeroX,
                LeftBoardZeroY = boardZeroY,
                FieldSizeX = fieldSize,
                FieldSizeY = fieldSize,
                SideFieldOffsetX = sideFieldOffset,
                SideFieldOffsetY = 0,
                CentralBoardZeroX = boardZeroX + 2 * fieldSize + sideFieldOffset,
                CentralBoardZeroY = boardZeroY,
                RightBoardZeroX = (boardZeroX + 2 * fieldSize + sideFieldOffset) + 8 * fieldSize + sideFieldOffset,
                RightBoardZeroY = boardZeroY
            };

            var serialConfiguration = new SerialConfiguration
            {
                BaudRate = 115200,
                SerialPortName = args.First()
            };

            var gpioConfiguration = new GpioConfiguration
            {
                Pin = RaspberryPi.Enums.Pins.Gpio3,
                InputType = RaspberryPi.Enums.InputType.Output,
                State = RaspberryPi.Enums.State.Low
            };

            var nodeHelper = new NodeHelper(boardConfiguration);
            var nodeMapper = new NodeMapper(boardConfiguration);
            var edgeMapper = new EdgeMapper(boardConfiguration, nodeHelper);
            var configurationManager = new ConfigurationManager(nodeMapper, edgeMapper, boardConfiguration, serialConfiguration, gpioConfiguration);
            IPathfinder pathfinder = new BreadthFirst();

            Console.WriteLine("Registering nodes and edges...");
            var nodes = configurationManager.MapBoard();

            Console.WriteLine("Configuring serial port...");
            var serial = configurationManager.InitializeSerialPort();

            Console.WriteLine("Configuring gpio pins...");
            var gpio = configurationManager.InitializeGpio();

            var controller = new Controller(gpio, serial);

            Console.WriteLine("Initializing SingalR...");
            var connection = new HubConnectionBuilder()
              .WithUrl("https://ghostchessweb.azurewebsites.net/chess?Password=P@ssw0rd&Board=true")
              .Build();

            Console.WriteLine("Moving to zero...");
            byte[] buffer = new byte[1024];
            serial.Open();
            while (true)
            {
                await serial.ReadAsync(buffer);
                var response = System.Text.Encoding.Default.GetString(buffer);
                if (response.Contains("start"))
                    break;
            }
            serial.DiscardOutBuffer();
            controller.Move(boardConfiguration.LeftBoardZeroX, boardConfiguration.LeftBoardZeroY).Sleep(1000);
            //.Sleep((int)(Vector.GetLength(LeftBoardZeroX, LeftBoardZeroY) * mmPerSec + AdditionalSecondSleep))
            //serial.Close();

            Console.WriteLine();

            var currentNode = nodes.First(t => t.Name.Equals("LI0"));

            await Task.Factory.StartNew(controller.Run, TaskCreationOptions.LongRunning);
            Console.WriteLine("Controller initialized...");

            connection.On<string, string>("Move", async (source, destination) =>
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine();
                Console.WriteLine($"Current node: {currentNode.Name}");
                Console.WriteLine($"Current x: {currentNode.X}");
                Console.WriteLine($"Current y: {currentNode.Y}");
                Console.WriteLine();

                var sourceNode = nodes.First(x => x.Name.Equals(source.ToUpper()));
                var destinationNode = nodes.First(x => x.Name.Equals(destination.ToUpper()));

                if (destinationNode.isEmpty == false)
                {
                    Node freeNode = null;
                    IEnumerable<Node> colorNodes;
                    var color = Abstractions.Enums.Colors.Black;

                    if (color == Abstractions.Enums.Colors.White)
                    {
                        colorNodes = nodeHelper.GetLeftCentralNodes(nodes);
                    }
                    else
                    {
                        colorNodes = nodeHelper.GetRightCentralNodes(nodes);
                    }

                    foreach (var node in colorNodes)
                    {
                        if (node.isEmpty == false)
                        {
                            freeNode = node;
                            break;
                        }
                    }

                    controller.Move(currentNode, destinationNode)
                        //.Sleep((int)(Vector.GetLength(currentNode, destinationNode) * mmPerSec + AdditionalXYSleep))
                        .Sleep(1000)
                        .MagnetOn();

                    var path = pathfinder.FindPath(destinationNode, freeNode);
                    for (int i = 1; i < path.Count(); i++)
                    {
                        var vector = new Vector(path.ElementAt(i - 1), path.ElementAt(i));
                        if (vector.X != 0 && vector.Y != 0)
                        {
                            controller.Move(path.ElementAt(i - 1), path.ElementAt(i));
                            //.Sleep((int)(vector.Length * mmPerSec + AdditionalXYSleep));
                        }
                        else
                        {
                            controller.Move(path.ElementAt(i - 1), path.ElementAt(i));
                            //.Sleep((int)(vector.Length * mmPerSec + AdditionalXSleep));
                        }
                    }
                    controller.Sleep(1000).MagnetOff();
                    currentNode = path.Last();
                }

                if (currentNode != sourceNode)
                {
                    controller.Move(currentNode, sourceNode);
                    //.Sleep((int)(Vector.GetLength(currentNode, sourceNode) * mmPerSec + AdditionalXYSleep));
                }

                if (sourceNode != destinationNode)
                {
                    controller.Sleep(1000).MagnetOn();
                    var path = pathfinder.FindPath(sourceNode, destinationNode);
                    for (int i = 1; i < path.Count(); i++)
                    {
                        var vector = new Vector(path.ElementAt(i - 1), path.ElementAt(i));
                        if (vector.X != 0 && vector.Y != 0)
                        {
                            controller.Move(path.ElementAt(i - 1), path.ElementAt(i));
                            //.Sleep((int)(vector.Length * mmPerSec + AdditionalXYSleep));
                        }
                        else
                        {
                            controller.Move(path.ElementAt(i - 1), path.ElementAt(i));
                            //.Sleep((int)(vector.Length * mmPerSec + AdditionalXSleep));
                        }
                    }
                    controller.Sleep(1000).MagnetOff();
                    currentNode = path.Last();
                }
            });

            await connection.StartAsync();
            Console.WriteLine("SignalR initialized.");
            Console.WriteLine("Ready!");
            await Task.Delay(-1);
        }
    }
}
