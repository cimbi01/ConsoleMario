using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleMario.Devices;
using System.Reflection;
using ConsoleMario.Utility;

namespace ConsoleMario
{
    internal static class LoadPath
    {
        #region Public Methods

        public static ConsoleMario.Utility.Path LoadPathFromFile(int level_number)
        {
            ConsoleMario.Utility.Path path;
            string filename = pathstring + "path" + level_number;
            // Read the preview
            string previewfile = filename + ".preview";
            string pathpreview = "";
            try
            {
                List<string> loadedpreview = ReadLines(previewfile, true);
                for (int i = 0; i < loadedpreview.Count; i++)
                {
                    pathpreview += loadedpreview[i];
                }
            }
            catch (FileNotFoundException) { };
            // Read ExamplePath
            string examplefile = filename + ".example";
            ConsoleMario.Utility.ExamplePath example = null;
            try
            {
                example = new ExamplePath(ReadPath(examplefile, level_number), pathpreview);
            }
            catch (FileNotFoundException) { }
            // Read Path
            string pathfile = filename + ".path";
            path = new Utility.Path(ReadPath(pathfile, level_number), example, pathpreview);
            return path;
        }
        public static int MaxPath()
        {
            string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            int path = 1;
            string pathstring = "path" + Convert.ToString(path);
            bool path_is_in_resources = true;
            while (path_is_in_resources)
            {
                path_is_in_resources = false;
                for (int i = 0; i < resources.Length; i++)
                {
                    if (resources[i].Split('.').Contains(pathstring))
                    {
                        i = resources.Length;
                        path_is_in_resources = true;
                    }
                }
                if (path_is_in_resources)
                {
                    path++;
                    pathstring = "path" + Convert.ToString(path);
                }
            }
            return path - 1;
        }

        #endregion Public Methods

        #region Private Fields

        private const string pathstring = "ConsoleMario.Paths.";

        #endregion Private Fields

        #region Private Methods

        private static List<string> ReadLines(string filename, bool preview = false)
        {
            if (Assembly.GetExecutingAssembly().GetManifestResourceNames().Contains(filename))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream(filename);
                StreamReader str = new StreamReader(stream);
                List<string> lines = new List<string>();
                string line = "";
                do
                {
                    line = str.ReadLine();
                    if (line != "" && line != null && preview)
                    {
                        line += '\n';
                    }
                    lines.Add(line);
                } while (line != "" && line != null);
                lines.Remove("");
                lines.Remove(null);
                str.Close();
                stream.Close();
                return lines;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        private static ConsoleMario.Utility.Path ReadPath(string filename, int level)
        {
            ConsoleMario.Utility.Path path = null;
            // Read Path
            List<string> loadeddevices = ReadLines(filename);
            string fileparams = filename + ".params";
            List<string> loadedparameters = null;
            try
            {
                loadedparameters = ReadLines(fileparams);
            }
            catch (FileNotFoundException) { }
            Device[,] devices = new Device[loadeddevices.Count, loadeddevices[0].ToCharArray().Length];
            int parameterindex = 0;
            for (int i = 0; i < devices.GetLength(0); i++)
            {
                // rows
                char[] rowdevices = loadeddevices[i].ToCharArray();
                // column
                for (int j = 0; j < rowdevices.Length; j++)
                {
                    if (devices[i, j] == null)
                    {
                        Device device = null;
                        if (rowdevices[j] != Key.KeyCharacter &&
                            rowdevices[j] != Spiral.SpiralCharacter)
                        {
                            device = Device.GetDeviceByCharacter(rowdevices[j]);
                        }
                        else
                        {
                            switch (rowdevices[j])
                            {
                                case Spiral.SpiralCharacter:
                                    device = Device.GetDeviceByCharacter(rowdevices[j], ref parameterindex, loadedparameters[parameterindex]);
                                    break;
                                case Key.KeyCharacter:
                                    // Door's row, columns separated by
                                    string[] positions = loadedparameters[parameterindex].Split(' ');
                                    int row = Convert.ToInt32(positions[0]);
                                    int col = Convert.ToInt32(positions[1]);
                                    devices[row-1, col-1] = new Door();
                                    device = Device.GetDeviceByCharacter(rowdevices[j], ref parameterindex, devices[row-1, col-1]);
                                    break;
                            }
                        }
                        devices[i, j] = device;
                    }
                }
            }
            path = new Utility.Path(devices, level);
            return path;
        }

        #endregion Private Methods
    }
}
