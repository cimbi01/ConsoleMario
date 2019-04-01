using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class End : Device
    {
        #region Public Constructors

        static End()
        {
            Device.AddCharDevicePair(EndCharacter, GetDevice);
        }

        #endregion Public Constructors

        #region Public Methods

        public static End GetDevice(object parameter2)
        {
            return new End();
        }

        #endregion Public Methods

        #region Public Fields

        // Defines the character of the End
        public const char EndCharacter = '#';

        #endregion Public Fields

        #region Public Constructors

        public End() : base(EndCharacter) { }

        #endregion Public Constructors

        #region Public Methods

        public override void Use(Player player)
        {
            player.Win = true;
        }

        #endregion Public Methods
    }
}
