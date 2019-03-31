using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleMario.Devices;

namespace ConsoleMario.Utility
{
    public class ExamplePath : Path
    {
        // preview the new Elements
        public string Preview { get; set; }
        public ExamplePath(Path _path, string preview) : base(_path.Devices, _path.LevelNumber)
        {
            Preview = preview;
        }
    }
}
