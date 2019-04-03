using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Fire : DeviceBase, IDevice
    {
        #region Public Fields

        // Defines the character of the Fire
        public const char FireCharacter = '^';

        #endregion Public Fields

        #region Public Constructors

        // set character
        public Fire() : base(FireCharacter) { }

        #endregion Public Constructors

        #region Public Methods

        public static Fire GetDevice(object parameter2)
        {
            return new Fire();
        }
        // Kill the player
        public new void Use(Player player)
        {
            player.Life = 0;
        }

        #endregion Public Methods
    }
}
