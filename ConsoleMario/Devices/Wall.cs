using ConsoleMario.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    // this exception throwned when player run in a wall
    public class RunInWallException : Exception
    {
        #region Public Constructors

        public RunInWallException() : base("You've run in a wall") { }

        #endregion Public Constructors
    }
    public class Wall : Device
    {
        #region Public Fields

        // Defines the character of the Wall
        public const char WallCharacter = 'I';

        #endregion Public Fields

        #region Public Constructors

        public Wall() : base(WallCharacter) { }

        #endregion Public Constructors

        #region Public Methods

        // throws a new RunInWallException
        public override void Use(Player player)
        {
            player.Life--;
            player.RenderNeeded = true;
            throw new RunInWallException();
        }

        #endregion Public Methods
    }
}
