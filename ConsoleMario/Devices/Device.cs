using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public abstract class Device
    {
        public char Character { get; protected set; }
        public abstract void Use(Player player);
        public Device(char ch) { Character = ch; }
        // ch is the char, parameterindex is the parameterindex of parameters, parameters are the parameters if exists
        public static Device GetDeviceByCharacter(char ch)
        {
            switch (ch)
            {
                case Street.StreetCharacter:
                    return new Street();
                case Wall.WallCharacter:
                    return new Wall();
                case Fire.FireCharacter:
                    return new Fire();
                case Door.DoorCharacter:
                    return new Door();
                case End.EndCharacter:
                    return new End();
            }
            return null;
        }
        // ch is the char, parameterindex is the parameterindex of parameters, parameters are the parameters if exists
        public static Device GetDeviceByCharacter(char ch, ref int paramaterindex, object parameters)
        {
            switch (ch)
            {
                case Street.StreetCharacter:
                    return new Street();
                case Wall.WallCharacter:
                    return new Wall();
                case Spiral.SpiralCharacter:
                    return new Spiral(Convert.ToInt32(parameters));
                case Fire.FireCharacter:
                    return new Fire();
                case Door.DoorCharacter:
                    return new Door();
                case Key.KeyCharacter:
                    return new Key((Door)Convert.ChangeType(parameters, typeof(Door)));
            }
            return null;
        }
    }
}
