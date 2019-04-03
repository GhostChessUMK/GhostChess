using GhostChess.Board.Abstractions.Commands;
using GhostChess.Board.Abstractions.Models;
using RJCP.IO.Ports;
using System;

namespace GhostChess.Board.Commands
{
    public class MoveCommand : ICommand
    {
        private SerialPortStream _serial;
        private Node _source, _destination;
        private double _x, _y;

        public MoveCommand(SerialPortStream serial, Node source, Node destination)
        {
            _serial = serial;
            _source = source;
            _destination = destination;
            var vector = GetMoveVector(_source, _destination);
            _x = vector.X;
            _y = vector.Y;
        }

        public MoveCommand(SerialPortStream serial, double x, double y)
        {
            _serial = serial;
            _x = x;
            _y = y;
        }

        public void Execute()
        {
            if (_source != null || _destination != null)
            {
                Console.WriteLine($"Moving: {_source.Name} -> {_destination.Name}");
            }
            else
            {
                Console.WriteLine($"Moving: {_x} -> {_y}");
            }
            //_serial.Open();
            _serial.WriteLine($"G00 X{_x} Y{_y}");
            //_serial.Close();
        }

        private Vector GetMoveVector(Node source, Node destination)
        {
            return new Vector(destination.X - source.X, destination.Y - source.Y);
        }
    }
}
