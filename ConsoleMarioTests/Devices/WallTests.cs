﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMario.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices.Tests
{
    [TestClass()]
    public class WallTests
    {
        [TestMethod()]
        [ExpectedException(typeof(RunInWallException))]
        public void UseTest()
        {
            Player player = new Player();
            Wall wall = new Wall();
            wall.Use(player);
        }
    }
}