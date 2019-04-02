using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Door : Device
    {
        private const char CharacterAfterStep = ' ';
        private bool characterchanged = false;

        // Defines the character of the Door
        public const char DoorCharacter = '¤';

        public Door() : base(DoorCharacter) { }

        // Describes if the Door is opened
        public bool Opened { get; set; } = false;

        public static Door GetDevice(object parameter2)
        {
            return new Door();
        }
        public override void Use(Player player)
        {
            // if the door is closed
            if (!Opened)
            {
                throw new DoorIsClosedException();
            }
            else if(!this.characterchanged)
            {
                Character = CharacterAfterStep;
                this.characterchanged = true;
            }
        }
    }
    public class DoorIsClosedException : Exception
    {
        public DoorIsClosedException() : base("The Door, which on you are is closed!") { }
    }
}
