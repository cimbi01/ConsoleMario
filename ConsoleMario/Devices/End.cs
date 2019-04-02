using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class End : Device
    {
        // Defines the character of the End
        public const char EndCharacter = '#';

        public End() : base(EndCharacter) { }

        public static End GetDevice(object parameter2)
        {
            return new End();
        }
        public override void Use(Player player)
        {
            player.Win = true;
        }
    }
}
