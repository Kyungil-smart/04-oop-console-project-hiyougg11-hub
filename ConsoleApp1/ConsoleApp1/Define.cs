 using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban_class
{
    internal class Define
    {
        public static readonly char PLAYER = 'P';
        public static readonly char BOX = 'B';
        public static readonly char GOAL = 'G';
        public static readonly char WALL = '#';
        public static readonly char EMPTY = ' ';
        public static readonly char BOX_ON_GOAL = '@';
        public static readonly char OBSTACLE = '■';

        public struct Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        /*
        public enum eMoveDir
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,

            NONE
        }
        */
    }
}
