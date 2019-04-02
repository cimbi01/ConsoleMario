using ConsoleMario.Devices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMario.Utility
{
    public class Path
    {
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
                throw new Exceptions.UtilityExceptions.NoMoreLevelException();
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
            bool exampleisnull = example == null;
            exampleisnull = exampleisnull || ExamplePath.Devices == null;
            if (exampleisnull)
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

        // Describes the Maximum types of level (can be an int from 1 to the max)
        public static int MaxLevel { get; } = LoadPath.MaxPath();
        // Describes the devices matrix We use this to Render to console
        public Device[,] Devices { get; protected set; }
        public Path ExamplePath { get; private set; } = null;
        public bool IsExample { get; private set; } = false;
        // Describes the levelnumber (from 1 to maxLevel)
        public int LevelNumber { get; protected set; }
        // preview the new Elements
        public string Preview { get; set; }
        public bool PreviewVisible { get; set; } = false;
    }
}
