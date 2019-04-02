using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Fire : Device
    {
        #region Public Methods

        public static Fire GetDevice(object parameter2)
        {
            return new Fire();
        }

        #endregion Public Methods

        #region Public Fields

        // Defines the character of the Fire
        public const char FireCharacter = '^';

        #endregion Public Fields

        #region Public Constructors

        // set character
        public Fire() : base(FireCharacter) { }

        #endregion Public Constructors

        #region Public Methods

        // Kill the player
        public override void Use(Player player)
        {
            player.Life = 0;
        }

        #endregion Public Methods
    }
}
