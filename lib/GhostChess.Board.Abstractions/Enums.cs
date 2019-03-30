using System;
using System.Collections.Generic;
using System.Text;

namespace GhostChess.Board.Abstractions
{
    public class Enums
    {
        public enum BoardNodeLetters
        {
            A = 1, B, C, D, E, F, G, H
        };

        public enum IntermediateNodeLetters
        {
            I = 0, J, K, L, M, N, O, P, R
        };

        public enum Colors
        {
            Black, White
        };
    }
}
