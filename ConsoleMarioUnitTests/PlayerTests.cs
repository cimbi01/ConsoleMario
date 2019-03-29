using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void MoveTestW()
        {
            Player player = new Player();
            player.Move('w');
            Assert.AreEqual(0, player.PositionX);
        }
        [TestMethod()]
        public void MoveTestA()
        {
            Player player = new Player();
            player.Move('a');
            Assert.AreEqual(0, player.PositionY);
        }
        [TestMethod()]
        public void MoveTestS()
        {
            Player player = new Player();
            player.Move('s');
            Assert.AreEqual(2, player.PositionX);
        }
        [TestMethod()]
        public void MoveTestD()
        {
            Player player = new Player();
            player.Move('d');
            Assert.AreEqual(2, player.PositionY);
        }
    }
}