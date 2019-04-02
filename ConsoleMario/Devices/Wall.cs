using ConsoleMario.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class Wall : Devices.Device
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
            throw new Exceptions.DeviceExceptions.RunInWallException();
        }
    }
}
