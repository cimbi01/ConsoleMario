using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMario.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Devices.Tests
{
    [TestClass()]
    public class SpiralTests
    {
        [TestMethod()]
        public void UseTest()
        {
            Player player = new Player();
            Spiral spiral = new Spiral(2);
            spiral.Use(player);
            Assert.AreEqual(-1, player.PositionX);
        }
    }
}