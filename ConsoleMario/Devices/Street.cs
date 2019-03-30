using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class Street : Device
    {
        // Defines the character of the Street
        public const char StreetChar = ' ';
        public Street() : base(StreetChar) {}

        public override void Use(Player player) {}
    }
}
