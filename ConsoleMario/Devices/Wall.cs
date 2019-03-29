using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class Wall : Device
    {
        public Wall() : base('*') { }
        // throws a new RunInWallException
        public override void Use(Player player)
        {
            player.Life--;
            throw new RunInWallException();
        }
    }
    // this exception throwned when player run in a wall
    public class RunInWallException : Exception
    {
        public RunInWallException() : base("You've run in a wall") { }
    }
}
