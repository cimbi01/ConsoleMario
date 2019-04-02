using System;

namespace ConsoleMario.Exceptions.DeviceExceptions
{
    public class DoorIsClosedException : Exception
    {
        #region Public Constructors

        public DoorIsClosedException() : base("The Door, which on you are is closed!") { }

        #endregion Public Constructors
    }
}
