using ConsoleMario.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    // this exception throwned when player run in a wall
    public class RunInWallException : Exception
    {
        public RunInWallException() : base("You've run in a wall") { }
    }
    public class Wall : Device
    {
        // Defines the character of the Wall
        public const char WallCharacter = 'I';

        public Wall() : base(WallCharacter) { }

        public static Wall GetDevice(object parameter2)
        {
            return new Wall();
        }
        // throws a new RunInWallException
        public override void Use(Player player)
        {
            player.Life--;
            throw new RunInWallException();
        }
    }
}
