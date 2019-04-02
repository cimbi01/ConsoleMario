using System;

namespace ConsoleMario.Exceptions.DeviceExceptions
{
    // this exception throwned when player run in a wall
    public class RunInWallException : Exception
    {
        public RunInWallException() : base("You've run in a wall") { }
    }
}
