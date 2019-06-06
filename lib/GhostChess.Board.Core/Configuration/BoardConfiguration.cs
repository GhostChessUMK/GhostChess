using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Core.Configuration
{
    public class BoardConfiguration
    {
        public double FieldSizeX { get; set; }
        public double FieldSizeY { get; set; }
        public double CentralBoardZeroX { get; set; }
        public double CentralBoardZeroY { get; set; }
        public double LeftBoardZeroX { get; set; }
        public double LeftBoardZeroY { get; set; }
        public double RightBoardZeroX { get; set; }
        public double RightBoardZeroY { get; set; }
        public double SideFieldOffsetX { get; set; }
        public double SideFieldOffsetY { get; set; }
    }
}
