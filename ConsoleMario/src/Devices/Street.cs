using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Street : DeviceBase, IDevice
    {
        #region Public Fields

        // Defines the character of the Street
        public const char StreetCharacter = ' ';

        #endregion Public Fields

        #region Public Constructors

        public Street() : base(StreetCharacter) { }

        #endregion Public Constructors

        #region Public Methods

        public static Street GetDevice(object parameter2)
        {
            return new Street();
        }
        public new void Use(Player player)
        {
            // implements IDevice.Use
        }

        #endregion Public Methods
    }
}
