using System;

namespace ConsoleMario.Exceptions.DeviceExceptions
{
    // this exception throwned when player run in a wall
    public class RunInWallException : Exception
    {
        #region Public Constructors

        public RunInWallException() : base("You've run in a wall") { }

        #endregion Public Constructors
    }
}
