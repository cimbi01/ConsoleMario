using ConsoleMario.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Utility
{
    public class NoMoreLevelException :Exception
    {
        #region Public Constructors

        public NoMoreLevelException() : base("There are no more level yet") { }

        #endregion Public Constructors
    }
    public class Path
    {
        #region Public Properties

        // preview the new Elements
        public string Preview { get; set; }
        public bool PreviewVisible { get; protected set; } = false;

        #endregion Public Properties

        #region Public Constructors

        // return a Path with level, row, and column used for PathLevel init
        public Path() { }
        public Path(int level)
        {
            if (level + 1 <= MaxLevel)
            {
                Path path = LoadPath.LoadPathFromFile(level + 1);
                ExamplePath = path.ExamplePath;
                LevelNumber = path.LevelNumber;
                Devices = path.Devices;
                Preview = path.Preview;
                PreviewVisible = path.PreviewVisible;
            }
            else
            {
                throw new NoMoreLevelException();
            }
        }
        public Path(Device[,] devices, int level, string preview = "")
        {
            Devices = devices;
            LevelNumber = level;
            Preview = preview;
        }
        public Path(Path _path, ExamplePath example, string _preview = "")
        {
            Preview = _preview;
            PreviewVisible = _path.PreviewVisible;
            Devices = _path.Devices;
            LevelNumber = _path.LevelNumber;
            ExamplePath = example;
            if (example == null || ExamplePath.Devices == null)
            {
                ExamplePath = null;
                PreviewVisible = true;
            }
        }

        #endregion Public Constructors

        #region Public Properties

        // Describes the Maximum types of level (can be an int from 1 to the max)
        public static int MaxLevel { get; } = LoadPath.MaxPath();
        // Describes the devices matrix We use this to Render to console And to use on the player if
        // steped on a Device
        public Device[,] Devices { get; protected set; }
        public ExamplePath ExamplePath { get; private set; } = null;
        // Describes the levelnumber (from 1 to maxLevel)
        public int LevelNumber { get; protected set; }

        #endregion Public Properties
    }
}
