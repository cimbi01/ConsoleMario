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
        #region Public Methods

        public static void AddCharDevicePair(char key, Func<object, Device> function)
        {
            charDevicePairs.Add(key, function);
        }
        public static void AddComplexCharacter(char ch)
        {
            complexDeviceChars.Add(ch);
        }

        #endregion Public Methods



        #region Public Methods

        // ch is the char, parameterindex is the parameterindex of parameters, parameters are the
        // parameters if exists
        public static Device GetDeviceByCharacter(char ch, ref int parameterindex, object parameters)
        {
            Device device = GetDeviceByCharacter(ch, parameters);
            if (complexDeviceChars.Contains(ch))
            {
                parameterindex++;
            }
            return device;
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

        #endregion Public Methods

        #region Private Fields

        private static Dictionary<char, Func<object, Device>> charDevicePairs = new Dictionary<char, Func<object, Device>>();
        private static List<char> complexDeviceChars = new List<char>();

        #endregion Private Fields

        #region Public Constructors

        protected Device(char ch) { Character = ch; }

        #endregion Public Constructors

        #region Public Properties

        #region Public Properties

        public char Character { get; protected set; }

        #endregion Public Properties

        #endregion Public Properties

        public abstract void Use(Player player);
    }
}
