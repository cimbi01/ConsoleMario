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
            for (int j = 0; j < this.column+1; j++)
            {
                Devices[0, j] = new Wall();
                Devices[this.row, j] = new Wall();
            }
            // 0 and last index add walls around
            for (int i = 0; i < this.row+1; i++)
            {
                Devices[i, 0] = (new Wall());
                Devices[i, this.column] = (new Wall());
            }
            // else add streets
            for (int i = 1; i < this.row; i++)
            {
                for (int j = 1; j < this.column; j++)
                {
                    Devices[i,j] = (new Street());
                }
            }
        }
        // Describes the Maximum types of level (can be an int from 1 to the max)
        public static int MaxLevel { get; private set; } = 1;
        // returns a List of Path from level1 path to levelMaxLevel
        public static List<Path> Init()
        {
            List<Path> paths = new List<Path>
            {
                Path1(),
                Path2(),
                Path3()
            };
            return paths;
        }
        // returns a level1 path
        public static Path Path1()
        {
            // level = 1
            // row*column 3 *3
            Path path = new Path(1, 3, 3);
            path.Devices[2, 2] = new End();
            return path;
        }
        public static Path Path2()
        {
            // level = 2
            // row*column 4*4
            MaxLevel++;
            Path path = new Path(2, 4, 4);
            path.Devices[3, 3] = new End();
            path.Devices[2, 4] = new Spiral(1);
            return path;
        }
        public static Path Path3()
        {
            // level = 3
            // row*column 5*5
            MaxLevel++;
            Path path = new Path(3, 5, 5);
            path.Devices[3, 3] = new End();
            path.Devices[1, 4] = new Wall();
            path.Devices[3, 4] = new Spiral(3);
            return path;
        }
    }
}
