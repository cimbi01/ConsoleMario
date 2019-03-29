using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class End : Device
    {
        public End() : base('#') {}
        public override void Use(Player player)
        {
            player.Win = true;
        }
    }
}
