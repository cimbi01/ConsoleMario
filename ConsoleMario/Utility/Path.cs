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
        public Path(bool example, Device[,] devices, int level, string preview = "")
        {
            PreviewVisible = example;
            IsExample = example;
            Devices = devices;
            LevelNumber = level;
            Preview = preview;
        }
        public Path(bool examplebool, Path _path, Path example, string _preview)
        {
            IsExample = examplebool;
            PreviewVisible = _path.PreviewVisible;
            Devices = _path.Devices;
            LevelNumber = _path.LevelNumber;
            ExamplePath = example;
            if (example == null || ExamplePath.Devices == null)
            {
                ExamplePath = null;
                PreviewVisible = true;
                Preview = _preview;
            }
        }
        public Path(bool examplebool, Path _path, string _preview)
        {
            IsExample = examplebool;
            Preview = _preview;
            PreviewVisible = _path.PreviewVisible;
            Devices = _path.Devices;
            LevelNumber = _path.LevelNumber;
        }

        #endregion Public Constructors

        #region Public Properties

        // Describes the Maximum types of level (can be an int from 1 to the max)
        public static int MaxLevel { get; } = LoadPath.MaxPath();
        // Describes the devices matrix We use this to Render to console
        public Device[,] Devices { get; protected set; }
        public Path ExamplePath { get; private set; } = null;
        public bool IsExample { get; private set; } = false;
        // Describes the levelnumber (from 1 to maxLevel)
        public int LevelNumber { get; protected set; }

        #endregion Public Properties
    }
}
