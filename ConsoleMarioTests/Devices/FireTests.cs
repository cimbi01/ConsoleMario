using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMario.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Devices.Tests
{
    [TestClass()]
    public class FireTests
    {
        [TestMethod()]
        public void UseTest()
        {
            Player player = new Player();
            Fire fire = new Fire();
            fire.Use(player);
            Assert.AreEqual(0, player.Life);
        }
    }
}
