using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class Key : Device
    {
        // Defines the character of the Key
        public const char KeyCharacter = '@';
        // Door to open
        readonly Door door;
        public Key(Door _door) : base(KeyCharacter) { this.door = _door; }

        public override void Use(Player player)
        {
            // opens the door
            this.door.Opened = true;
        }
    }
}
