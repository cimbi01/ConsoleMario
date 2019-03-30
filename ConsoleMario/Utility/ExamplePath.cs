using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Devices;

namespace ConsoleMario
{
    public class ExamplePath : Path
    {
        // preview the new Elements
        public string Preview { get; private set; }
        public ExamplePath(ExamplePath _path)
        {
            if (_path != null)
            {
                Preview = "This is An example of the new Elements: \n";
                Devices = new Device[_path.Row + 1, _path.Column + 1];
                LevelNumber = _path.LevelNumber;
                Row = _path.Row;
                Column = _path.Column;
                Build();
                Preview = _path.Preview; Devices = _path.Devices;
            }
        }
        public ExamplePath(int _level, int _row, int _column) : base(_level, _row, _column) {}
        // return the 1st ExamplePath of the Leveled Path
        public static ExamplePath ExamplePath1()
        {
            ExamplePath example = new ExamplePath(1, 3, 3);
            example.Preview = example.Preview + Wall.WallCharacter + " is the Wall\n" +
                        "If you run in a Wall Element, you lose a Life Pont\n" +
                        End.EndCharacter + " is the goal, you have to reach it before you lose 5 Life point to Win";
            example.Devices[example.Row - 1, example.Column - 1] = new End();
            return example;
        }
        public static ExamplePath ExamplePath2()
        {
            ExamplePath example = new ExamplePath(2, 4, 4);
            example.Preview = example.Preview + Spiral.SpiralCharacter + " is the Spiral\n" +
                "If you run in a Spiral Element, you jump up by the force of the spiral\n";
            example.Devices[2,2] = new Spiral(1);
            example.Devices[example.Row - 1, example.Column - 1] = new End();
            return example;
        }
    }
}
