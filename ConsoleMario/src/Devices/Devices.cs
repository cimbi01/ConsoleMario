using System;
using System.Collections.Generic;

namespace ConsoleMario.Devices
{
    public static class Devices
    {
        #region Public Constructors

        static Devices()
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

        #endregion Public Constructors

        #region Public Methods

        // ch is the char, parameterindex is the parameterindex of parameters, parameters are the
        // parameters if exists
        public static IDevice GetDeviceByCharacter(char ch, object parameters = null)
        {
            charDevicePairs.TryGetValue(ch, out Func<object, IDevice> getdevice);
            return getdevice(parameters);
        }
        public static bool IsComplexCharacter(char ch)
        {
            return (complexDeviceChars.Contains(ch));
        }

        #endregion Public Methods

        #region Private Fields

        private static Dictionary<char, Func<object, IDevice>> charDevicePairs = new Dictionary<char, Func<object, IDevice>>();
        private static List<char> complexDeviceChars = new List<char>();

        #endregion Private Fields
    }
}
