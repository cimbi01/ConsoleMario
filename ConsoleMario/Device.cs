using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario
{
    abstract class Device
    {
        public string Character { get; }
        public abstract void Use(Player player);
    }
}
