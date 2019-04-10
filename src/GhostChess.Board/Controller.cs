﻿using GhostChess.Board.Abstractions.Commands;
using GhostChess.Board.Abstractions.Controller;
using GhostChess.Board.Abstractions.Models;
using GhostChess.Board.Commands;
using GhostChess.RaspberryPi;
using RJCP.IO.Ports;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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

        public IController Move(double x, double y)
        {
            commandList.Add(new MoveCommand(_serial, x, y));
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

        public async Task Execute()
        {
            foreach(var command in commandList)
            {
                await command.Execute();
                Thread.Sleep(250);
            }
            commandList.Clear();
        }
    }
}
