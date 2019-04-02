using System;

namespace ConsoleMario.Exceptions.DeviceExceptions
{
    public class DoorIsClosedException : Exception
    {
        public DoorIsClosedException() : base("The Door, which on you are is closed!") { }
    }
}
