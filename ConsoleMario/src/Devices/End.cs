﻿using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class End : DeviceBase, IDevice
    {
        #region Public Fields

        // Defines the character of the End
        public const char EndCharacter = '#';

        #endregion Public Fields

        #region Public Constructors

        public End() : base(EndCharacter) { }

        #endregion Public Constructors

        #region Public Methods

        public static End GetDevice(object parameter2)
        {
            return new End();
        }
        public new void Use(Player player)
        {
            player.Win = true;
        }

        #endregion Public Methods
    }
}
