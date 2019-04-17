using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Core.Configuration
{
    public class SerialConfiguration
    {
        public int BaudRate { get; set; } 
        public string SerialPortName { get; set; }
    }
}
