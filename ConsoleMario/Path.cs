using ConsoleMario.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario
{
    public class Path
    {
        // row and column
        private readonly int row, column;
        // Describes the levelnumber (from 1 to maxLevel)
        public int LevelNumber { get; private set; }
        // Describes if the path is completed
        public bool Completed { get; private set; } = false;
        // Describes the devices matrix
        // We use this to Render to console
        // And to use on the player if steped on a Device
        public List<List<Device>> Devices { get; } = new List<List<Device>>();
        // returns a new Path by the levelnumer
        // Each levelnumbered Path has a static method to call
        public Path(int _levelnumber)
        {

        }
        // Init the Devices
        private void Build()
        {
            for (int i = 0; i < this.row; i++)
            {
                Devices.Add(new List<Device>());
                for (int j = 0; j < this.column; j++)
                {
                    Devices[i].Add(new Street());
                }
            }
        }
        // Describes the Maximum types of level (can be an int from 1 to the max)
        public const int MaxLevel = 1;
        // returns a List of Path from level1 path to levelMaxLevel
        public static List<Path> Init()
        {
            List<Path> paths = new List<Path>();
            for (int i = 0; i < MaxLevel; i++)
            {
                paths.Add(new Path(i + 1));
            }
            return paths;
        }

    }
}
