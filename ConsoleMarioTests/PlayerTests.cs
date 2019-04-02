using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Utility;

namespace ConsoleMario.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void MoveTestA()
        {
            Player player = new Player();
            int expx = player.PositionX;
            int expy = player.PositionY;
            player.Move(ConsoleKey.A);
            Assert.AreEqual(0, player.PositionY);
            Assert.AreEqual(expx, player.PositionX);
            Assert.AreEqual(1, expy);
            Assert.AreEqual(1, expx);
        }
        [TestMethod()]
        public void MoveTestD()
        {
            Player player = new Player();
            int expx = player.PositionX;
            int expy = player.PositionY;
            player.Move(ConsoleKey.D);
            Assert.AreEqual(expx, player.PositionX);
            Assert.AreEqual(2, player.PositionY);
            Assert.AreEqual(1, expy);
            Assert.AreEqual(1, expx);
        }
        [TestMethod()]
        public void MoveTestS()
        {
            Player player = new Player();
            int expx = player.PositionX;
            int expy = player.PositionY;
            player.Move(ConsoleKey.S);
            Assert.AreEqual(2, player.PositionX);
            Assert.AreEqual(player.PositionY, expy);
            Assert.AreEqual(1, expy);
            Assert.AreEqual(1, expx);
        }
        [TestMethod()]
        public void MoveTestW()
        {
            Player player = new Player();
            int expx = player.PositionX;
            int expy = player.PositionY;
            player.Move(ConsoleKey.W);
            Assert.AreEqual(0, player.PositionX);
            Assert.AreEqual(player.PositionY, expy);
            Assert.AreEqual(1, expy);
            Assert.AreEqual(1, expx);
        }
    }
}
