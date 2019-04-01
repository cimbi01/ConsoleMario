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
    public class DoorTests
    {
        #region Public Methods

        [TestMethod()]
        [ExpectedException(typeof(DoorIsClosedException))]
        public void UseTestLocked()
        {
            Player player = new Player();
            Door door = new Door();
            Key key = new Key(door);
            door.Use(player);
            Assert.AreEqual(false, door.Opened);
        }

        #endregion Public Methods
    }
}
