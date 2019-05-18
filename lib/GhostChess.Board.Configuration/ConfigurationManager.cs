using GhostChess.Board.Abstractions.Configuration;
using GhostChess.Board.Configuration.Mappers;
using GhostChess.Board.Core.Configuration;
using GhostChess.Board.Core.Models;
using GhostChess.RaspberryPi;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostChess.Board.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly NodeMapper _nodeMapper;
        private readonly EdgeMapper _edgeMapper;
        public BoardConfiguration BoardConfiguration { get; }
        public SerialConfiguration SerialConfiguration { get; }
        public GpioConfiguration GpioConfiguration { get; }

        public ConfigurationManager(NodeMapper nodeMapper, EdgeMapper edgeMapper, BoardConfiguration boardConfiguration, 
            SerialConfiguration serialConfiguration, GpioConfiguration gpioConfiguration)
        {
            _nodeMapper = nodeMapper;
            _edgeMapper = edgeMapper;

            BoardConfiguration = boardConfiguration;
            SerialConfiguration = serialConfiguration;
            GpioConfiguration = gpioConfiguration;
        }

        public IEnumerable<Node> MapBoard()
        {
            var nodes = _nodeMapper.Map();
            _edgeMapper.Map(nodes);
            return nodes;
        }

        public SerialPortStream InitializeSerialPort()
        {
            return new SerialPortStream(SerialConfiguration.SerialPortName, SerialConfiguration.BaudRate);
        }

        public Gpio InitializeGpio()
        {
            return new Gpio(GpioConfiguration.Pin, GpioConfiguration.InputType, GpioConfiguration.State);
            //return new Gpio(GpioConfiguration.Pin);
        }
    }
}
