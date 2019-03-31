﻿using System;
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
    static class LoadPath
    {
        private const string pathstring = "ConsoleMario.Paths.";
        public static ConsoleMario.Utility.Path LoadPathFromFile(int level_number)
        {
            ConsoleMario.Utility.Path path;
            string filename = pathstring + "path" + level_number;
            // Read the preview
            string previewfile = filename + ".preview";
            List<string> loadedpreview = ReadLines(previewfile, true);
            string pathpreview = "";
            for (int i = 0; i < loadedpreview.Count; i++)
            {
                pathpreview += loadedpreview[i];
            }
            // Read ExamplePath
            string examplefile = filename + ".example";
            ConsoleMario.Utility.ExamplePath example = new ExamplePath(ReadPath(examplefile, level_number), pathpreview);
            // Read Path
            string pathfile = filename + ".path";
            path = new Utility.Path(ReadPath(pathfile, level_number), example);
            return path;
        }
        private static List<string> ReadLines(string filename, bool preview = false)
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
        private static ConsoleMario.Utility.Path ReadPath(string filename, int level)
        {
            ConsoleMario.Utility.Path path = null;
            // Read Path
            List<string> loadeddevices = ReadLines(filename);
            string fileparams = filename + ".params";
            List<string> loadedparameters = null;
            if (File.Exists(fileparams))
            {
                loadedparameters = ReadLines(fileparams);
            }
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
    }
}
