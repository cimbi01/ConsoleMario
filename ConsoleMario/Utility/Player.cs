using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Utility
{
    public class Player
    {
        #region Public Fields

        public const int MAXLIFE = 5;

        #endregion Public Fields

        #region Public Properties

        // default Console Background Color and Default Color to change on cursor
        public static char DefaultCharacter { get; set; } = '+';
        // up, down, righ left keyboard key init
        public static ConsoleKey DOWN { get; set; } = ConsoleKey.S;
        public static ConsoleKey LEFT { get; set; } = ConsoleKey.A;
        public static ConsoleKey RIGHT { get; set; } = ConsoleKey.D;
        public static ConsoleKey UP { get; set; } = ConsoleKey.W;
        // Character character on console to write
        public char Character { get; set; } = DefaultCharacter;
        public bool ExamplePathWin { get; set; } = false;
        public int Life { get; set; } = MAXLIFE;
        // Character position X,Y on console to write row
        public int PositionX { get; private set; } = 1;
        //column
        public int PositionY { get; private set; } = 1;
        // and previous X,Y on console
        //previous row
        public int PreviousPositionX { get; private set; } = 1;
        //previous column
        public int PreviousPositionY { get; private set; } = 1;
        public bool RenderNeeded { get; set; } = false;
        // Describes if the player won the current PathLevel
        public bool Win { get; set; } = false;

        #endregion Public Properties

        #region Public Methods

        public void Move(ConsoleKey ch)
        {
            if (ch == UP)
            { Move(-1, 0); }
            else if (ch == DOWN)
            { Move(1, 0); }
            else if (ch == RIGHT)
            { Move(0, 1); }
            else if (ch == LEFT)
            { Move(0, -1); }
        }
        public void Move(int x, int y)
        {
            PreviousPositionX = PositionX;
            PreviousPositionY = PositionY;
            PositionX += x;
            PositionY += y;
        }
        public void Reset()
        {
            Win = false;
            Life = Player.MAXLIFE;
            PreviousPositionX = PositionX = 1;
            PreviousPositionY = PositionY = 1;
        }
        public void StepBack()
        {
            // step back
            if (PositionX - PreviousPositionX > 0)
            {
                Move(-1, 0);
            }
            else if (PositionX - PreviousPositionX < 0)
            {
                Move(1, 0);
            }
            else if(PositionY - PreviousPositionY > 0)
            {
                Move(0, -1);
            }
            else if (PositionY - PreviousPositionY < 0)
            {
                Move(0, 1);
            }
        }

        #endregion Public Methods
    }
}
