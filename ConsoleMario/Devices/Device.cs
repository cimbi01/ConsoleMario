using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario
{
    public abstract class Device
    {
        public char Character { get; protected set; }
        public abstract void Use(Player player);
    }
}
