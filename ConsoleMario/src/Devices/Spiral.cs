using ConsoleMario.Utility;
using System;

namespace ConsoleMario.Devices
{
    public class Spiral : Device
    {
        #region Public Fields

        // Defines the character of the Spiral
        public const char SpiralCharacter = '_';

        #endregion Public Fields

        #region Public Constructors

        public Spiral(int _force) : base(SpiralCharacter) { this.force = _force; }

        #endregion Public Constructors

        #region Public Methods

        public static Spiral GetDevice(object parameter2)
        {
            return new Spiral(Convert.ToInt32(parameter2));
        }
        // make player move by force
        public override void Use(Player player)
        {
            player.Move(-this.force, 0);
        }

        #endregion Public Methods

        #region Private Fields

        // how much should the player jump;
        private readonly int force;

        #endregion Private Fields
    }
}
