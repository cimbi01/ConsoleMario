namespace ConsoleMario.Devices
{
    public class DeviceBase : IDevice
    {
        #region Protected Constructors

        protected DeviceBase(char ch)
        {
            Character = ch;
        }

        #endregion Protected Constructors

        #region Public Properties

        public char Character { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void Use(ConsoleMario.Utility.Player player)
        {
            // implements IDevice
        }

        #endregion Public Methods
    }
}
