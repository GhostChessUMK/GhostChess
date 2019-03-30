using GhostChess.Board.Abstractions.Commands;
using GhostChess.Board.Abstractions.Controller;
using GhostChess.Board.Abstractions.Models;
using GhostChess.Board.Commands;
using GhostChess.RaspberryPi;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GhostChess.Board
{
    public class Controller : IController
    {
        private IList<ICommand> commandList;
        private SerialPortStream _serial;
        private Gpio _gpio;

        public Controller(Gpio gpio, SerialPortStream serial)
        {
            _gpio = gpio;
            _serial = serial;
            commandList = new List<ICommand>();
        }

        public IController Move(Node source, Node destination)
        {
            commandList.Add(new MoveCommand(_serial, source, destination));
            return this;
        }

        public IController Sleep(int miliseconds)
        {
            commandList.Add(new SleepCommand(miliseconds));
            return this;
        }

        public IController MagnetOn()
        {
            commandList.Add(new MagnetOnCommand(_gpio));
            return this;
        }

        public IController MagnetOff()
        {
            commandList.Add(new MagnetOffCommand(_gpio));
            return this;
        }

        public void Execute()
        {
            foreach(var command in commandList)
            {
                command.Execute();
            }
            commandList.Clear();
        }
    }
}
