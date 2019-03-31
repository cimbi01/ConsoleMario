using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleMario.Devices;
using ConsoleMario.Utility;

namespace ConsoleMario.Paths
{
    static class LoadPath
    {
        public static ConsoleMario.Utility.Path LoadPathFromFile(int level_number)
        {
            ConsoleMario.Utility.Path path;
            string filename = "Path" + level_number;
            StreamReader streamReader = new StreamReader(filename);
            // Read the preview
            List<string> loadedpreview = ReadLines(streamReader);
            string pathpreview = "";
            for (int i = 0; i < loadedpreview.Count; i++)
            {
                pathpreview += loadedpreview[i];
            }
            // Read Path
            ConsoleMario.Utility.ExamplePath example = new ExamplePath(ReadPath(streamReader), pathpreview);
            path = new Utility.Path(ReadPath(streamReader), example);
            streamReader.Close();
            return path;
        }
        private static List<string> ReadLines(StreamReader streamReader)
        {
            List<string> lines = new List<string>();
            string line = "";
            do
            {
                line = streamReader.ReadLine();
                lines.Add('n' + line);
            } while (line != "");
            lines.Remove("");
            return lines;
        }
        private static ConsoleMario.Utility.Path ReadPath(StreamReader streamReader)
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
                    if (devices[i, j] != null)
                    {
                        Device device = null;
                        if (rowdevices[j] != Key.KeyCharacter)
                        {
                            device = Device.GetDeviceByCharacter(rowdevices[j], ref parameterindex, loadedparameters[parameterindex]);
                        }
                        else
                        {
                            // Door's row, columns separated by 
                            string[] positions = loadedparameters[parameterindex].Split(' ');
                            int row = Convert.ToInt32(positions[0]);
                            int col = Convert.ToInt32(positions[1]);
                            devices[row, col] = new Door();
                            device = Device.GetDeviceByCharacter(rowdevices[j], ref parameterindex, devices[row, col]);
                        }
                        devices[i, j] = device;
                    }
                }
            }
            return path;
        }
    }
}
