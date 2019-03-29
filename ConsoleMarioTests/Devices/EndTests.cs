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
    public class EndTests
    {
        [TestMethod()]
        public void UseTest()
        {
            Player player = new Player();
            End end = new End();
            end.Use(player);
            Assert.AreEqual(true, player.Win);
        }
        [TestMethod()]
        public void UseTest2()
        {
            Player player = new Player();
            End end = new End();
            Assert.AreEqual(false, player.Win);
        }
    }
}