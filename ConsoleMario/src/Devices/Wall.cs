using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Wall : Devices.Device
    {
        #region Public Fields

        // Defines the character of the Wall
        public const char WallCharacter = 'I';

        #endregion Public Fields

        #region Public Constructors

        public Wall() : base(WallCharacter) { }

        #endregion Public Constructors

        #region Public Methods

        public static Wall GetDevice(object parameter2)
        {
            return new Wall();
        }
        // throws a new RunInWallException
        public override void Use(Player player)
        {
            player.Life--;
            throw new Exceptions.DeviceExceptions.RunInWallException();
        }

        #endregion Public Methods
    }
}
