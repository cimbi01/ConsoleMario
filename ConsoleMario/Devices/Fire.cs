using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Fire : Device
    {
        // Defines the character of the Fire
        public const char FireCharacter = '^';

        // set character
        public Fire() : base(FireCharacter) { }

        public static Fire GetDevice(object parameter2)
        {
            return new Fire();
        }
        // Kill the player
        public override void Use(Player player)
        {
            player.Life = 0;
        }
    }
}
