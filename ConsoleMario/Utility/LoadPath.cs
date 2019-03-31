using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleMario.Devices;

namespace ConsoleMario.Utility
{
    static class LoadPath
    {
        public static Path LoadPathFromFile(int level_number)
        {
            Path path = new Path();
            string filename = "/Paths/Path" + level_number;
            StreamReader streamReader = new StreamReader(filename);
            // Read the preview
            List<string> loadedpreview = ReadLines(streamReader);
            string pathpreview = "";
            for (int i = 0; i < loadedpreview.Count; i++)
            {
                pathpreview += loadedpreview[i];
            }
            // Read examplepath
            List<string> loadeddevices = ReadLines(streamReader);
            List<List<Device>> devices = new List<List<Device>>();
            for (int i = 0; i < loadeddevices.Count; i++)
            {
                // rows
                char[] rowdevices = loadeddevices[i].ToCharArray();
                devices.Add(new List<Device>());
                // column
                for (int j = 0; j < rowdevices.Length; j++)
                {
                }
            }
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
            return lines;
        }
    }
}
