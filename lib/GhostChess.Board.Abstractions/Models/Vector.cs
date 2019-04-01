using System;

namespace GhostChess.Board.Abstractions.Models
{
    public class Vector
    {
        public double X { get; }
        public double Y { get; }
        public double Length { get; }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
            Length = GetLength(X, Y);
        }

        public Vector(Node source, Node destination)
        {
            X = destination.X - source.X;
            Y = destination.Y - source.Y;
            Length = GetLength(X, Y);
        }

        public static double GetLength(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        public static double GetLength(Node source, Node destination)
        {
            double x, y;
            x = destination.X - source.X;
            y = destination.Y - source.Y;
            return Math.Sqrt(x * x + y * y);
        }
    }
}
