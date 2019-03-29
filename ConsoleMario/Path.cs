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
        // Describes the devices matrix
        // We use this to Render to console
        // And to use on the player if steped on a Device
        public Device[,] Devices { get; }
        // return a Path with level, row, and column
        // used for PathLevel init
        public Path(int _level, int _row, int _column)
        {
            Devices = new Device[_row+1, _column+1];
            LevelNumber = _level;
            this.row = _row;
            this.column = _column;
            Build();
        }
        public Path(Path _path)
        {
            Devices = _path.Devices;
            LevelNumber = _path.LevelNumber;
            this.row = _path.row;
            this.column = _path.column;
        }
        // Init the Devices of the Path add walls on the first and last outer indexes 
        // and on the first and last inner indexes
        // and streets as default on all outher indexes
        private void Build()
        {
            // 0 and last index add walls around
            for (int j = 0; j < this.column; j++)
            {
                Devices[0, j] = new Wall();
                Devices[this.row, j] = new Wall();
            }
            // 0 and last index add walls around
            for (int i = 0; i < this.row; i++)
            {
                Devices[i, 0] = (new Wall());
                Devices[i, this.column] = (new Wall());
            }
            // else add streets
            for (int i = 1; i < this.row+1; i++)
            {
                for (int j = 1; j < this.column; j++)
                {
                    Devices[i,j] = (new Street());
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
            path.Devices[3, 3] = new End();
            return path;
        }
    }
}
