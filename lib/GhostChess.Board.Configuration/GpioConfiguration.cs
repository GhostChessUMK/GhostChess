using GhostChess.RaspberryPi;
using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Configuration
{
    public class GpioConfiguration
    {
        public RaspberryPi.Enums.Pins Pin { get; set; }
        public RaspberryPi.Enums.InputType InputType { get; set; }
        public RaspberryPi.Enums.State State { get; set; }
    }
}
