using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class Fire : Device
    {
        // set character
        public Fire()
        {
            Character = '^';
        }
        // Kill the player
        public override void Use(Player player)
        {
            player.Live = false;
        }
    }
}
