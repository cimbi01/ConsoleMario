﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices
{
    public class Spiral : Device
    {

        // how much should the player jump;
        private readonly int force;
        public Spiral() { Character = '_'; }
        // make player move by force
        public override void Use(Player player)
        {
            player.Move(this.force, 0);
        }
    }
}
