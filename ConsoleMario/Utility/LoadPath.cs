using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleMario.Devices;
using ConsoleMario.Utility;

namespace ConsoleMario
{
    static class LoadPath
    {
        private static readonly string pathstring = @"Paths\";
        public static ConsoleMario.Utility.Path LoadPathFromFile(int level_number)
        {
            ConsoleMario.Utility.Path path;
            string filename = pathstring + "Path" + level_number;
            StreamReader streamReader = new StreamReader(filename);
            // Read the preview
            List<string> loadedpreview = ReadLines(streamReader, true);
            string pathpreview = "";
            for (int i = 0; i < loadedpreview.Count; i++)
            {
                pathpreview += loadedpreview[i];
            }
            // Read ExamplePath
            ConsoleMario.Utility.ExamplePath example = new ExamplePath(ReadPath(streamReader, level_number), pathpreview);
            // Read Path
            path = new Utility.Path(ReadPath(streamReader, level_number), example);
            streamReader.Close();
            return path;
        }
        private static List<string> ReadLines(StreamReader streamReader, bool preview = false)
        {
            List<string> lines = new List<string>();
            string line = "";
            do
            {
                line = streamReader.ReadLine();
                if (line != "" && preview)
                {
                    line += '\n';
                }
                lines.Add(line);
            } while (line != "" && line != null);
            lines.Remove("\n");
            lines.Remove("");
            return lines;
        }
        private static ConsoleMario.Utility.Path ReadPath(StreamReader streamReader, int level)
        {
            ConsoleMario.Utility.Path path = null;
            // Read Path
            List<string> loadeddevices = ReadLines(streamReader);
            List<string> loadedparameters = ReadLines(streamReader);
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
                        if (rowdevices[j] != Key.KeyCharacter ||
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
                                    devices[row, col] = new Door();
                                    device = Device.GetDeviceByCharacter(rowdevices[j], ref parameterindex, devices[row, col]);
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
        public static int GetMaxLevel()
        {
            int maxlevel = System.IO.Directory.GetFiles(@"Paths\").Length;
            return maxlevel;
        }
    }
}
