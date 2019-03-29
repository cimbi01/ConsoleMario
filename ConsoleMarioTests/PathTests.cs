using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Devices;

namespace ConsoleMario.Tests
{
    [TestClass()]
    public class PathTests
    {
        [TestMethod()]
        public void BuildTest()
        {
            Path path = new Path(1, 3, 2);
            // path.Build();
            Assert.AreEqual(1, path.LevelNumber);
            Assert.AreEqual(3 + 1, path.Devices.GetLength(0));
            Assert.AreEqual(2 + 1, path.Devices.GetLength(1));
        }

        [TestMethod()]
        public void Path1Test()
        {
            Player player = new Player();
            Path path = Path.Path1();
            player.Move(2, 2);
            Assert.AreEqual(false, player.Win);
            path.Devices[player.PositionX, player.PositionY].Use(player);
            Assert.AreEqual(true, player.Win);
        }
    }
}