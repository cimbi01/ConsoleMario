using ConsoleMario.Utility;
using System;

namespace ConsoleMario.Devices
{
    public class Key : DeviceBase, IDevice
    {
        #region Public Fields

        // Defines the character of the Key
        public const char KeyCharacter = '@';

        #endregion Public Fields

        #region Public Constructors

        public Key(Door _door) : base(KeyCharacter) { this.door = _door; }

        #endregion Public Constructors

        #region Public Methods

        public static Key GetDevice(object parameter2)
        {
            Door door = (Door)Convert.ChangeType(parameter2, typeof(Door));
            return new Key(door);
        }
        public new void Use(Player player)
        {
            // opens the door
            this.door.Opened = true;
            if (!this.characterchanged)
            {
                Character = CharacterAfterStep;
                this.characterchanged = true;
            }
        }

        #endregion Public Methods

        #region Private Fields

        private const char CharacterAfterStep = ' ';
        // Door to open
        private readonly Door door;
        private bool characterchanged = false;

        #endregion Private Fields
    }
}
