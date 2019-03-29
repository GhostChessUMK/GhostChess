using RJCP.IO.Ports;
using System;
using System.Threading;
using ChessboardSteering.Configuration;
using ChessboardSteering.Nodes;
using System.Linq;
using System.Text;
using ChessboardSteering.Helpers;
using GhostChess.RaspberryPi;
using System.Collections;
using System.Collections.Generic;
//using System.IO.Ports;

namespace ChessboardSteering
{
    class Program
    {
        static void Main(string[] args)
        {
            //var serial = new SerialPort("ttyUSB0", 115200);
            //SerialPortStream serial = new SerialPortStream("/dev/ttyACM0", 9600);

            Console.WriteLine("Configuring board...");

            Constants constants = new Constants(60, 20, 40, 40, 10, 0);
            RegisterNodes registerNodes = new RegisterNodes();
            RegisterEdges registerEdges = new RegisterEdges();
            Pathfinder pathfinder = new Pathfinder();

            Console.WriteLine("Registering nodes...");
            var nodes = registerNodes.Register();
            Console.WriteLine("Registering edges...");
            registerEdges.Register(nodes);

            Console.WriteLine("Configuring serial port...");
            SerialPortStream serial = new SerialPortStream("COM4", 115200);           

            Console.WriteLine("Configuring gpio pins...");
            Gpio gpio = new Gpio(GhostChess.RaspberryPi.Enums.Pins.Gpio3);
            var controller = new ChessboardSteering.Controller.Controller(gpio, serial);

            Console.WriteLine("Moving to zero...");
            serial.Open();
            Thread.Sleep(5000);
            serial.WriteLine($"G00 X{Constants.LeftBoardZeroX} Y{Constants.LeftBoardZeroY}");
            Thread.Sleep((int)(Vector.GetLength(Constants.LeftBoardZeroX, Constants.LeftBoardZeroY) * 60 + 1000));
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
                    var color = Helpers.Enums.Colors.Black;

                    if(color == Helpers.Enums.Colors.White)
                    {
                        colorNodes = GetLeftCentralNodes(nodes);
                    }
                    else
                    {
                        colorNodes = GetRightCentralNodes(nodes);
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
                        .Sleep((int)(Vector.GetLength(currentNode, destinationNode) * 60 + 300))
                        .MagnetOn();

                    var path = pathfinder.FindPath(destinationNode, freeNode);
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

                if(currentNode != sourceNode)
                {
                    controller.Move(currentNode, sourceNode).Sleep((int)(Vector.GetLength(currentNode, sourceNode) * 60 + 300));
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

        private static List<Node> GetRightCentralNodes(List<Node> nodes)
        {
            List<Node> rightNodes = new List<Node>();
            foreach(var node in nodes)
            {
                if(node.Name.StartsWith("RA") || node.Name.StartsWith("RB"))
                {
                    rightNodes.Add(node);
                }    
            }
            return rightNodes;
        }

        private static List<Node> GetLeftCentralNodes(List<Node> nodes)
        {
            List<Node> leftNodes = new List<Node>();
            foreach (var node in nodes)
            {
                if (node.Name.StartsWith("LA") || node.Name.StartsWith("LB"))
                {
                    leftNodes.Add(node);
                }
            }
            return leftNodes;
        }
    }
}
