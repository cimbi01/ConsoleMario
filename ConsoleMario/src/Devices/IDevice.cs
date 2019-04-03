namespace ConsoleMario.Devices
{
    public interface IDevice
    {
        #region Public Methods

        char Character { get; set; }
        void Use(ConsoleMario.Utility.Player player);

        #endregion Public Methods
    }
}
