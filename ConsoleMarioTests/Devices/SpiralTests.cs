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
            int positionx = player.PositionX;
            int positiony = player.PositionY;
            int expx = player.PreviousPositionX;
            int expy = player.PreviousPositionY;
            spiral.Use(player);
            Assert.AreEqual(-1, player.PositionX);
            Assert.AreEqual(positiony, player.PositionY);
            Assert.AreEqual(positiony, expy);
            Assert.AreEqual(1, expx);
        }
    }
}