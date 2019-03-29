using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario
{
    public class Player
    {
        // Describes if the player won the current PathLevel
        public bool Win { get; set; } = false;
        // up, down, righ left keyboard key init
        private static readonly char UP = 'w', DOWN = 's', RIGHT = 'd', LEFT = 'a';
        // Character live
        public bool Live { get; internal set; } = true;
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
            if (ch == UP)
            { Move(-1, 0); }
            else if (ch == DOWN)
            { Move(1, 0); }
            else if (ch == RIGHT)
            { Move(0, 1); }
            else if (ch == LEFT)
            { Move(0, -1); }
            // THROW NOTUSEDKEYEXCEPTION
            else
            { }
        }
        public void Move(int x, int y)
        {
            PreviousPositionX = PositionX;
            PreviousPositionY = PositionY;
            PositionX += x;
            PositionY += y;
        }
    }
}
