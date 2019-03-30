using RJCP.IO.Ports;
using System;
using System.Threading;
using System.Linq;
using System.Text;
using GhostChess.RaspberryPi;
using System.Collections;
using System.Collections.Generic;
using GhostChess.Board.Abstractions;
using GhostChess.Board.Abstractions.Configuration;
using GhostChess.Board.Abstractions.Models;
using GhostChess.Board.Abstractions.Helpers;
using static GhostChess.Board.Abstractions.Constants;

namespace GhostChess.Board
{
    class Program
    {
        private const int ERROR_BAD_ARGUMENTS = 0xA0;

        static void Main(string[] args)
        {
            if(args.Count().Equals(0))
            {
                Console.WriteLine("Error! Put serial port name in arguments ex.");
                Console.WriteLine("sudo dotnet GhostChess.Board.dll /dev/ttyUSB0");
                Environment.Exit(ERROR_BAD_ARGUMENTS);
            }
            Console.WriteLine("Configuring board...");
            Constants constants = new Constants(args.First(), 60, 20, 40, 40, 10, 0);
            RegisterNodes registerNodes = new RegisterNodes();
            RegisterEdges registerEdges = new RegisterEdges();
            Pathfinder pathfinder = new Pathfinder();

            Console.WriteLine("Registering nodes...");
            var nodes = registerNodes.Register();

            Console.WriteLine("Registering edges...");
            registerEdges.Register(nodes);

            Console.WriteLine("Configuring serial port...");
            SerialPortStream serial = new SerialPortStream(SerialPortName, BaudRate);           

            Console.WriteLine("Configuring gpio pins...");
            Gpio gpio = new Gpio(RaspberryPi.Enums.Pins.Gpio3);
            var controller = new Controller(gpio, serial);

            Console.WriteLine("Moving to zero...");
            serial.Open();
            Thread.Sleep(5000);
            serial.WriteLine($"G00 X{LeftBoardZeroX} Y{LeftBoardZeroY}");
            Thread.Sleep((int)(Vector.GetLength(LeftBoardZeroX, LeftBoardZeroY) * mmPerSec + AdditionalSecondSleep));
            //serial.Close();

            Console.WriteLine("Ready!");
            Console.WriteLine();

            var currentNode = nodes.First(t => t.Name.Equals("LI0"));

            while (true)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine();
                Console.WriteLine($"Current node: {currentNode.Name}");
                Console.WriteLine($"Current x: {currentNode.X}");
                Console.WriteLine($"Current y: {currentNode.Y}");
                Console.WriteLine();
                Console.Write("Put source: ");
                string source = Console.ReadLine();
                Console.Write("Put destination: ");
                string destination = Console.ReadLine();
                Console.WriteLine();

                var sourceNode = nodes.First(x => x.Name.Equals(source));
                var destinationNode = nodes.First(x => x.Name.Equals(destination));

                if (destinationNode.isEmpty == false)
                {
                    Node freeNode = null;
                    List<Node> colorNodes;
                    var color = Abstractions.Enums.Colors.Black;

                    if(color == Abstractions.Enums.Colors.White)
                    {
                        colorNodes = NodeHelper.GetLeftCentralNodes(nodes);
                    }
                    else
                    {
                        colorNodes = NodeHelper.GetRightCentralNodes(nodes);
                    }

                    foreach(var node in colorNodes)
                    {
                        if(node.isEmpty == false)
                        {
                            freeNode = node;
                            break;
                        }
                    }

                    controller.Move(currentNode, destinationNode)
                        .Sleep((int)(Vector.GetLength(currentNode, destinationNode) * mmPerSec + AdditionalXYSleep))
                        .MagnetOn();

                    var path = pathfinder.FindPath(destinationNode, freeNode);
                    for(int i = 1; i < path.Count; i++)
                    {
                        var vector = new Vector(path[i - 1], path[i]);
                        if(vector.X != 0 && vector.Y != 0)
                        {
                            controller.Move(path[i - 1], path[i]).Sleep((int)(vector.Length * mmPerSec + AdditionalXYSleep));
                        }
                        else
                        {
                            controller.Move(path[i - 1], path[i]).Sleep((int)(vector.Length * mmPerSec + AdditionalXSleep));
                        }
                    }
                    controller.MagnetOff();
                    currentNode = path.Last();
                }

                if(currentNode != sourceNode)
                {
                    controller.Move(currentNode, sourceNode).Sleep((int)(Vector.GetLength(currentNode, sourceNode) * mmPerSec + AdditionalXYSleep));
                }

                if(sourceNode != destinationNode)
                {
                    controller.MagnetOn();
                    var path = pathfinder.FindPath(sourceNode, destinationNode);
                    for(int i = 1; i < path.Count; i++)
                    {
                        var vector = new Vector(path[i - 1], path[i]);
                        if(vector.X != 0 && vector.Y != 0)
                        {
                            controller.Move(path[i - 1], path[i]).Sleep((int)(vector.Length * 60 + 300));
                        }
                        else
                        {
                            controller.Move(path[i - 1], path[i]).Sleep((int)(vector.Length * 60 + 75));
                        }
                    }
                    controller.MagnetOff();
                    currentNode = path.Last();
                }

                controller.Execute();
            }
        }
    }
}
