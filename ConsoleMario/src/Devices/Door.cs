using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Door : DeviceBase, IDevice
    {
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

        public static Door GetDevice(object parameter2)
        {
            return new Door();
        }

        public new void Use(Player player)
        {
            // if the door is closed
            if (!Opened)
            {
                throw new Exceptions.DeviceExceptions.DoorIsClosedException();
            }
            else
            {
                TryChangeCharacter();
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void TryChangeCharacter()
        {
            if (!this.characterchanged)
            {
                Character = CharacterAfterStep;
                this.characterchanged = true;
            }
        }

        #endregion Private Methods

        #region Private Fields

        private const char CharacterAfterStep = ' ';
        private bool characterchanged = false;

        #endregion Private Fields
    }
}
