using GhostChess.Board.Abstractions.Configuration;
using GhostChess.Board.Configuration.Mappers;
using GhostChess.Board.Models;
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
        private readonly BoardConfiguration _boardConfiguration;
        private readonly SerialConfiguration _serialConfiguration;
        private readonly GpioConfiguration _gpioConfiguration;

        public ConfigurationManager(NodeMapper nodeMapper, EdgeMapper edgeMapper, BoardConfiguration boardConfiguration, 
            SerialConfiguration serialConfiguration, GpioConfiguration gpioConfiguration)
        {
            _nodeMapper = nodeMapper;
            _edgeMapper = edgeMapper;

            _boardConfiguration = boardConfiguration;
            _serialConfiguration = serialConfiguration;
            _gpioConfiguration = gpioConfiguration;
        }

        public IEnumerable<Node> MapBoard()
        {
            List<Node> nodes = _nodeMapper.Map().ToList();
            _edgeMapper.Map(nodes);
            return nodes;
        }

        public SerialPortStream InitializeSerialPort()
        {
            return new SerialPortStream(_serialConfiguration.SerialPortName, _serialConfiguration.BaudRate);
        }

        public Gpio InitializeGpio()
        {
            return new Gpio(_gpioConfiguration.Pin, _gpioConfiguration.InputType, _gpioConfiguration.State);
        }
    }
}
