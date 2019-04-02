using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Door : Device
    {
        #region Public Methods

        public static Door GetDevice(object parameter2)
        {
            return new Door();
        }

        #endregion Public Methods

        #region Private Fields

        private const char CharacterAfterStep = ' ';

        #endregion Private Fields

        #region Public Fields

        // Defines the character of the Door
        public const char DoorCharacter = '¤';

        #endregion Public Fields

        #region Public Constructors

        public Door() : base(DoorCharacter) { }

        #endregion Public Constructors

        #region Public Properties

        // Describes if the Door is opened
        public bool Opened { get; set; } = false;

        #endregion Public Properties

        #region Public Methods

        public override void Use(Player player)
        {
            // if the door is closed
            if (!Opened)
            {
                throw new DoorIsClosedException();
            }
            else
            {
                Character = CharacterAfterStep;
            }
        }

        #endregion Public Methods
    }
    public class DoorIsClosedException : Exception
    {
        #region Public Constructors

        public DoorIsClosedException() : base("The Door, which on you are is closed!") { }

        #endregion Public Constructors
    }
}
