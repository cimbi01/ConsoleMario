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
        #region Public Constructors

        public ExamplePath(Path _path, string preview) : base(_path.Devices, _path.LevelNumber)
        {
            Preview = preview;
        }

        #endregion Public Constructors

        #region Public Properties

        // preview the new Elements
        public string Preview { get; set; }

        #endregion Public Properties
    }
}
