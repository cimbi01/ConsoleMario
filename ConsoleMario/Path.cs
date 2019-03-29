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
        // row and column without wall-s around
        private readonly int row, column;
        // Describes the levelnumber (from 1 to maxLevel)
        public int LevelNumber { get; private set; }
        // Describes if the path is completed
        public bool Completed { get; private set; } = false;
        // Describes the devices matrix
        // We use this to Render to console
        // And to use on the player if steped on a Device
        public List<List<Device>> Devices { get; } = new List<List<Device>>();
        // return a Path with level, row, and column
        // used for PathLevel init
        public Path(int _level, int _row, int _column)
        {
            LevelNumber = _level;
            this.row = _row;
            this.column = _column;
            Build();
        }
        // Init the Devices of the Path add walls on the first and last outer indexes 
        // and on the first and last inner indexes
        // and streets as default on all outher indexes
        private void Build()
        {
            for (int i = 0; i < this.row+1; i++)
            {
                Devices.Add(new List<Device>());
                // if i = 0 or i is on the last index then add walls around
                if (i == 0 || i == this.row)
                {
                    Devices[i].Add(new Wall());
                }
                // else add streets
                else
                { 
                    for (int j = 0; j < this.column+1; j++)
                    {
                        // if j = 0 or j is on the last index then add walls around
                        if (j == 0 || j == this.column)
                        {
                            Devices[i].Add(new Wall());
                        }
                        // else add streets
                        else
                        {
                            Devices[i].Add(new Street());
                        }
                    }
                }
            }
        }
        // Describes the Maximum types of level (can be an int from 1 to the max)
        public const int MaxLevel = 1;
        // returns a List of Path from level1 path to levelMaxLevel
        public static List<Path> Init()
        {
            List<Path> paths = new List<Path>
            {
                Path1()
            };
            return paths;
        }
        // returns a level1 path
        public static Path Path1()
        {
            // level = 1
            // row*column 3 *3
            Path path = new Path(1, 3, 3);
            return path;
        }
    }
}
