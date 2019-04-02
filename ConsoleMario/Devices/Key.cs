using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices
{
    public class Key : Device
    {
        private const char CharacterAfterStep = ' ';
        // Door to open
        private readonly Door door;
        private bool characterchanged = false;

        // Defines the character of the Key
        public const char KeyCharacter = '@';

        public Key(Door _door) : base(KeyCharacter) { this.door = _door; }

        public static Key GetDevice(object parameter2)
        {
            Door door = (Door)Convert.ChangeType(parameter2, typeof(Door));
            return new Key(door);
        }
        public override void Use(Player player)
        {
            // opens the door
            this.door.Opened = true;
            if (!this.characterchanged)
            {
                Character = CharacterAfterStep;
                this.characterchanged = true;
            }
        }
    }
}
