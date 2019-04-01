using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Utility
{
    internal class Position
    {
        #region Private Fields

        internal int X { get; private set; }
        internal int Y { get; private set; }

        #endregion Private Fields

        #region Public Constructors

        public Position(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }

        #endregion Public Constructors
    }
    public class Player
    {
        #region Public Constructors

        public Player()
        {
            Init();
        }

        #endregion Public Constructors

        #region Public Methods

        public static void Init()
        {
            key_move_pairs = new Dictionary<ConsoleKey, Position>
            {
                { UP, new Position(-1, 0) },
                { DOWN, new Position(1, 0) },
                { RIGHT, new Position(0, 1) },
                { LEFT, new Position(0, -1) }
            };
        }

        #endregion Public Methods

        #region Private Fields

        private static Dictionary<ConsoleKey, Position> key_move_pairs;

        #endregion Private Fields

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
            try
            {
                key_move_pairs.TryGetValue(ch, out Position position);
                int x = position.X;
                int y = position.Y;
                Move(x, y);
            }
            catch (NullReferenceException) { }
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
            int posx = 0, posy = 0;
            int y = PositionY - PreviousPositionY;
            int x = PositionX - PreviousPositionX;
            try
            {
                posx = -1*(x / Math.Abs(x));
            }
            catch (DivideByZeroException)
            {
                posy = -1 * (y / Math.Abs(y));
            }
            RenderNeeded = true;
            Move(posx, posy);
        }

        #endregion Public Methods
    }
}
