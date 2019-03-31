using ConsoleMario.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class Street : Device
    {
        #region Public Fields

        // Defines the character of the Street
        public const char StreetCharacter = ' ';

        #endregion Public Fields

        #region Public Constructors

        public Street() : base(StreetCharacter) { }

        #endregion Public Constructors

        #region Public Methods

        public override void Use(Player player) { }

        #endregion Public Methods
    }
}
