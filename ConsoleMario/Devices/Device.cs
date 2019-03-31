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
        #region Public Constructors

        public Device(char ch) { Character = ch; }

        #endregion Public Constructors

        #region Public Properties

        public char Character { get; protected set; }

        #endregion Public Properties

        #region Public Methods

        // ch is the char, parameterindex is the parameterindex of parameters, parameters are the
        // parameters if exists
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
        // ch is the char, parameterindex is the parameterindex of parameters, parameters are the
        // parameters if exists
        public static Device GetDeviceByCharacter(char ch, ref int parameterindex, object parameters)
        {
            switch (ch)
            {
                case Spiral.SpiralCharacter:
                    parameterindex++;
                    return new Spiral(Convert.ToInt32(parameters));
                case Key.KeyCharacter:
                    parameterindex++;
                    return new Key((Door)Convert.ChangeType(parameters, typeof(Door)));
            }
            return null;
        }
        public abstract void Use(Player player);

        #endregion Public Methods
    }
}
