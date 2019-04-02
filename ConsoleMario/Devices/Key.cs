using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Key : Device
    {
        #region Public Methods

        public static Key GetDevice(object parameter2)
        {
            Door door = (Door)Convert.ChangeType(parameter2, typeof(Door));
            return new Key(door);
        }

        #endregion Public Methods

        #region Private Fields

        private const char CharacterAfterStep = ' ';

        #endregion Private Fields

        #region Public Fields

        // Defines the character of the Key
        public const char KeyCharacter = '@';

        #endregion Public Fields

        #region Public Constructors

        public Key(Door _door) : base(KeyCharacter) { this.door = _door; }

        #endregion Public Constructors

        #region Public Methods

        public override void Use(Player player)
        {
            // opens the door
            this.door.Opened = true;
            Character = CharacterAfterStep;
        }

        #endregion Public Methods

        #region Private Fields

        // Door to open
        private readonly Door door;

        #endregion Private Fields
    }
}
