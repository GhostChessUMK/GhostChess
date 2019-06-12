using GhostChess.Board.Abstractions;
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
        //TODO: Move to gamehandler class, create logger with Logger.Logs for more information and/or adding to file, (perhaps return stream)
        //TODO: Template pattern (program flow), Initialize, Execute ^ (simmilar)
        //TODO: Clean up SignalR, move parts of code to separate methods
        private const int ERROR_BAD_ARGUMENTS = 0xA0;

        public static async Task Main(string[] args)
        {
            if (args.Count().Equals(0))
            {
                Logger.Log("Error! Put serial port name in arguments ex.");
                Logger.Log("sudo dotnet GhostChess.Board.dll /dev/ttyUSB0");
                Logger.Log("./run.sh /dev/ttyUSB0");
                Environment.Exit(ERROR_BAD_ARGUMENTS);
            }

            Logger.Log("Configuring board...");
            var fieldSize = 50;
            var boardZeroX = 7;
            var boardZeroY = 53;
            var sideFieldOffset = 7.5;
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

            Logger.Log("Configuring serial port...");
            var serialConfiguration = new SerialConfiguration
            {
                BaudRate = 115200,
                SerialPortName = args.First()
            };

            Logger.Log("Configuring GPIO...");
            var gpioConfiguration = new GpioConfiguration
            {
                Pin = RaspberryPi.Enums.Pins.Gpio14,
                InputType = RaspberryPi.Enums.InputType.Output,
                State = RaspberryPi.Enums.State.Low
            };

            Logger.Log("Initializing board...");
            var pathfinder = new BreadthFirst();
            var nodeMapper = new NodeMapper(boardConfiguration);
            var edgeMapper = new EdgeMapper(boardConfiguration);
            var configurationManager = new ConfigurationManager(nodeMapper, edgeMapper, boardConfiguration, serialConfiguration, gpioConfiguration);

            var nodes = configurationManager.MapBoard();
            var gpio = configurationManager.InitializeGpio();
            var serial = configurationManager.InitializeSerialPort();

            Logger.Log("Configuring connection...");
            var controller = new Controller(gpio, serial);
            var connection = new HubConnectionBuilder()
              .WithUrl("https://ghostchessweb.azurewebsites.net/chess?Password=P@ssw0rd&Board=true")
              .Build();

            var gameHandler = new GameHandler(nodes, gpio, serial, controller, pathfinder, configurationManager, connection);

            Logger.Log("Starting game...");
            _ = Task.Factory.StartNew(controller.Run, TaskCreationOptions.LongRunning);
            _ = Task.Factory.StartNew(gameHandler.Run, TaskCreationOptions.LongRunning);    
            
            await Task.Delay(-1);
        }
    }
}
