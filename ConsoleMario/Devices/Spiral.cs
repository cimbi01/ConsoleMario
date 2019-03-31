using ConsoleMario.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class Spiral : Device
    {
        // Defines the character of the Spiral
        public const char SpiralCharacter = '_';
        // how much should the player jump;
        private readonly int force;
        public Spiral(int _force) : base(SpiralCharacter) { this.force = _force;}
        // make player move by force
        public override void Use(Player player)
        {
            player.Move(-this.force, 0);
            player.RenderNeeded = true;
        }
    }
}
