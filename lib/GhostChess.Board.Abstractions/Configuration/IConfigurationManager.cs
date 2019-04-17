using GhostChess.Board.Models;
using GhostChess.RaspberryPi;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Abstractions.Configuration
{
    public interface IConfigurationManager
    {
        IEnumerable<Node> MapBoard();
        SerialPortStream InitializeSerialPort();
        Gpio InitializeGpio();
    }
}
