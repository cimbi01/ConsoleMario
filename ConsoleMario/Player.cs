using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario
{
    public class Player
    {
        // up, down, righ left keyboard key init
        private static readonly char UP = 'w', DOWN = 's', RIGHT = 'd', LEFT = 'a';
        // Character live
        public bool Live { get; private set; } = true;
        // Character character on console to write
        public char Character { get; } = '+';
        // Character position X,Y on console to write
        // row
        public int PositionX { get; private set; } = 1;
        //column
        public int PositionY { get; private set; } = 1;
        // and previous X,Y on console
        //previous row
        public int PreviousPositionX { get; private set; } = 1;
        //previous column
        public int PreviousPositionY { get; private set; } = 1;
        // move x, y positions
        // DOESNT HANDLE UPPERCASES!!! :(
        public void Move(char ch)
        {
            PreviousPositionX = PositionX;
            PreviousPositionY = PositionY;
            if (ch == UP)
            { PositionX--; }
            else if (ch == DOWN)
            { PositionX++; }
            else if (ch == RIGHT)
            { PositionY++; }
            else if (ch == LEFT)
            { PositionY--; }
            // THROW NOTUSEDKEYEXCEPTION
            else
            { }
        }
    }
}
