using GhostChess.Board.Core.Configuration;
using GhostChess.Board.Core.Models;
using GhostChess.RaspberryPi;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Abstractions.Configuration
{
    public interface IConfigurationManager
    {
        BoardConfiguration BoardConfiguration { get; }
        SerialConfiguration SerialConfiguration { get; }
        GpioConfiguration GpioConfiguration { get; }
        IEnumerable<Node> MapBoard();
        SerialPortStream InitializeSerialPort();
        Gpio InitializeGpio();
    }
}
