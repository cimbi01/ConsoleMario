using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Utility
{
    internal static class Program
    {
        #region Public Methods

        public static void Main(string[] args)
        {
            Game.Play();
            Console.WriteLine("Press a Key to exit!");
            Console.ReadKey();
        }

        #endregion Public Methods
    }
}
