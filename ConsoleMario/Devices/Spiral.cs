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
        // how much should the player jump;
        private readonly int force;

        // Defines the character of the Spiral
        public const char SpiralCharacter = '_';

        public Spiral(int _force) : base(SpiralCharacter) { this.force = _force; }

        public static Spiral GetDevice(object parameter2)
        {
            return new Spiral(Convert.ToInt32(parameter2));
        }
        // make player move by force
        public override void Use(Player player)
        {
            player.Move(-this.force, 0);
        }
    }
}
