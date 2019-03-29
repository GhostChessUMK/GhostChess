using ChessboardSteering.Nodes;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ChessboardSteering.Commands
{
    public class MoveCommand : ICommand
    {
        private SerialPortStream _serial;
        private Node _source, _destination;

        public MoveCommand(SerialPortStream serial, Node source, Node destination)
        {
            _serial = serial;
            _source = source;
            _destination = destination;
        }

        public void Execute()
        {
            var vector = GetMoveVector(_source, _destination);
            Console.WriteLine($"Moving: {_source.Name} -> {_destination.Name}");
            //_serial.Open();
            _serial.WriteLine($"G00 X{vector.X} Y{vector.Y}");
            //_serial.Close();
        }

        private Vector GetMoveVector(Node source, Node destination)
        {
            return new Vector(destination.X - source.X, destination.Y - source.Y);
        }
    }
}
