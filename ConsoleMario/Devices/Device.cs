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
        private static Dictionary<char, Func<object, Device>> charDevicePairs = new Dictionary<char, Func<object, Device>>();
        private static List<char> complexDeviceChars = new List<char>();
        private char character;

        protected Device(char ch) { Character = ch; }

        static Device()
        {
            charDevicePairs.Add(Door.DoorCharacter, Door.GetDevice);
            charDevicePairs.Add(End.EndCharacter, End.GetDevice);
            charDevicePairs.Add(Fire.FireCharacter, Fire.GetDevice);
            charDevicePairs.Add(Key.KeyCharacter, Key.GetDevice);
            complexDeviceChars.Add(Key.KeyCharacter);
            charDevicePairs.Add(Spiral.SpiralCharacter, Spiral.GetDevice);
            complexDeviceChars.Add(Spiral.SpiralCharacter);
            charDevicePairs.Add(Street.StreetCharacter, Street.GetDevice);
            charDevicePairs.Add(Wall.WallCharacter, Wall.GetDevice);
        }

        public char Character
        {
            get
            {
                return this.character;
            }
            protected set
            {
                this.character = value;
                Render.RenderPlayer();
            }
        }

        // ch is the char, parameterindex is the parameterindex of parameters, parameters are the
        // parameters if exists
        public static Device GetDeviceByCharacter(char ch, object parameters = null)
        {
            charDevicePairs.TryGetValue(ch, out Func<object, Device> getdevice);
            return getdevice(parameters);
        }
        public static bool IsComplexCharacter(char ch)
        {
            return (complexDeviceChars.Contains(ch));
        }
        public abstract void Use(Player player);
    }
}
