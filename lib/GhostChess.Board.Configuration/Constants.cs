namespace GhostChess.Board.Configuration
{
    public class Constants
    {
        //TODO: Cleanup / remove private set
        public const double mmPerSec = 75;
        public const double AdditionalXSleep = 150;
        public const double AdditionalXYSleep = 600;
        public const double AdditionalSecondSleep = 1000;
        public const int BaudRate = 115200;
        public static string SerialPortName { get; private set;  }

        public const double MachineZeroX = 0;
        public const double MachineZeroY = 0;
        public static double CentralBoardZeroX { get; private set; }
        public static double CentralBoardZeroY { get; private set; }

        public static double LeftBoardZeroX { get; private set; }
        public static double LeftBoardZeroY { get; private set; }

        public static double RightBoardZeroX { get; private set; }
        public static double RightBoardZeroY { get; private set; }

        public static double FieldSizeX { get; private set; }
        public static double FieldSizeY { get; private set; }

        public static double SideFieldOffsetX { get; private set; }
        public static double SideFieldOffsetY { get; private set; }

        public Constants(string serialPortName, double boardZeroX, double boardZeroY, double fieldSizeX, double fieldSizeY, 
            double sideFieldsOffsetX, double sideFieldsOffsetY)
        {
            SerialPortName = serialPortName;

            FieldSizeX = fieldSizeX;
            FieldSizeY = fieldSizeY;

            LeftBoardZeroX = boardZeroX;
            LeftBoardZeroY = boardZeroY;

            CentralBoardZeroX = LeftBoardZeroX + 2 * FieldSizeX + sideFieldsOffsetX;
            CentralBoardZeroY = LeftBoardZeroY;

            RightBoardZeroX = CentralBoardZeroX + 8 * FieldSizeX + sideFieldsOffsetX;
            RightBoardZeroY = LeftBoardZeroY;

            SideFieldOffsetX = sideFieldsOffsetX;
            SideFieldOffsetY = sideFieldsOffsetY;
        }
    }
}
