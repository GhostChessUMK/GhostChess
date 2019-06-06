using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[{DateTime.Now.ToShortTimeString()}] ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Info] > ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{message}\n");
        }
    }
}
