﻿using GhostChess.Board.Abstractions;
using GhostChess.Board.Abstractions.Configuration;
using GhostChess.Board.Configuration.Mappers;
using GhostChess.Board.Configuration;
using GhostChess.RaspberryPi;
using Microsoft.AspNetCore.SignalR.Client;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostChess.Board.Algorithms.Pathfinders;
using GhostChess.Board.Abstractions.Pathfinders;
using GhostChess.Board.Core.Configuration;
using GhostChess.Board.Core.Models;

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

            var pathfinder = new BreadthFirst();
            var nodeMapper = new NodeMapper(boardConfiguration);
            var edgeMapper = new EdgeMapper(boardConfiguration);
            var configurationManager = new ConfigurationManager(nodeMapper, edgeMapper, boardConfiguration, serialConfiguration, gpioConfiguration);

            var nodes = configurationManager.MapBoard();
            var gpio = configurationManager.InitializeGpio();
            var serial = configurationManager.InitializeSerialPort();

            var controller = new Controller(gpio, serial);
            var connection = new HubConnectionBuilder()
              .WithUrl("https://ghostchessweb.azurewebsites.net/chess?Password=P@ssw0rd&Board=true")
              .Build();

            var gameHandler = new GameHandler(nodes, gpio, serial, controller, pathfinder, configurationManager, connection);

            await Task.Factory.StartNew(controller.Run, TaskCreationOptions.LongRunning);
            await Task.Factory.StartNew(gameHandler.Run, TaskCreationOptions.LongRunning);    

            await connection.StartAsync();
            await Task.Delay(-1);
        }
    }
}
