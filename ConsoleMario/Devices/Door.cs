using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class Door : Device
    {
        // Defines the character of the Door
        public const char DoorCharacter = '¤';
        // Describes if the Door is opened
        public bool Opened { get; set; } = false;
        public Door() : base(DoorCharacter) {}

        public override void Use(Player player)
        {
            // if the door is closed
            if (!Opened)
            {
                player.Life--;
                player.RenderNeeded = true;
                throw new DoorIsClosedException();
            }
        }
    }
    public class DoorIsClosedException : Exception
    {
        public DoorIsClosedException() : base("The Door, which on you are is closed!") { }
    }
}
