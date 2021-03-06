﻿using GhostChess.Board.Abstractions.Commands;
using GhostChess.Board.Abstractions.Controller;
using GhostChess.Board.Configuration;
using GhostChess.Board.Commands;
using GhostChess.RaspberryPi;
using RJCP.IO.Ports;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GhostChess.Board.Core.Models;
using System;

namespace GhostChess.Board
{
    public class Controller : IController
    {
        private Queue<ICommand> commandList;
        private SerialPortStream _serial;
        private Gpio _gpio;

        public Controller(Gpio gpio, SerialPortStream serial)
        {
            _gpio = gpio;
            _serial = serial;
            commandList = new Queue<ICommand>();
        }

        public IController Move(Node source, Node destination)
        {
            commandList.Enqueue(new MoveCommand(_serial, source, destination));
            return this;
        }

        public IController Move(double x, double y)
        {
            commandList.Enqueue(new MoveCommand(_serial, x, y));
            return this;
        }

        public IController Sleep(int miliseconds)
        {
            commandList.Enqueue(new SleepCommand(miliseconds));
            return this;
        }

        public IController MagnetOn()
        {
            commandList.Enqueue(new MagnetOnCommand(_gpio));
            return this;
        }

        public IController MagnetOff()
        {
            commandList.Enqueue(new MagnetOffCommand(_gpio));
            return this;
        }

        public async Task Run()
        {
            Logger.Log("Awaiting commands...");
            Logger.Log($"Enqueued commands: {commandList.Count}");
            while (true)
            {
                if(commandList.TryDequeue(out var command))
                {
                    Logger.Log($"Enqueued commands: {commandList.Count}");
                    await command.Execute();
                }
                await Task.Delay(250);
            }
        }
    }
}
