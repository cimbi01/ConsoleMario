using ConsoleMario.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleMario.Utility
{
    internal static class LoadPath
    {
        #region Private Fields

        private const string pathstring = "ConsoleMario.Paths.";
        private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

        #endregion Private Fields

        #region Public Methods

        public static ConsoleMario.Utility.Path LoadPathFromFile(int level_number)
        {
            string filename = pathstring + "path" + level_number;
            string preview = "";
            // load preview
            string previewfilename = filename + ".preview";
            try
            {
                List<string> readpreview = ReadLines(previewfilename);
                for (int i = 0; i < readpreview.Count; i++)
                {
                    preview += readpreview[i] + '\n';
                }
                preview = preview.Remove(preview.Length - 1, 1);
            }
            catch (System.IO.FileNotFoundException) { }
            Path examplepath = ReadPath(preview, filename, level_number, ".example", true);
            Path path = ReadPath(preview, filename, level_number, ".path", false);
            return new Path(false, path, examplepath, preview);
        }
        public static int MaxPath()
        {
            string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            int pathlevel = 1;
            string pathfilename = "path" + Convert.ToString(pathlevel);
            bool path_is_in_resources = true;
            while (path_is_in_resources)
            {
                path_is_in_resources = false;
                for (int i = 0; i < resources.Length &&
                    !path_is_in_resources; i++)
                {
                    path_is_in_resources = resources[i].Split('.').Contains(pathfilename);
                }
                if (path_is_in_resources)
                {
                    pathlevel++;
                    pathfilename = "path" + Convert.ToString(pathlevel);
                }
            }
            return pathlevel - 1;
        }

        #endregion Public Methods

        #region Private Methods

        private static Device[,] GetDevices(List<string> loadeddevices, List<string> loadedparameters)
        {
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
                        PushDeviceToDevices(devices, rowdevices, loadedparameters, i, j, ref parameterindex);
                    }
                }
            }
            return devices;
        }
        private static Device[,] GetDevices(List<string> loadeddevices)
        {
            Device[,] devices = new Device[loadeddevices.Count, loadeddevices[0].ToCharArray().Length];
            for (int i = 0; i < devices.GetLength(0); i++)
            {
                // rows
                char[] rowdevices = loadeddevices[i].ToCharArray();
                // column
                for (int j = 0; j < rowdevices.Length; j++)
                {
                    Device device = Device.GetDeviceByCharacter(rowdevices[j]);
                    devices[i, j] = device;
                }
            }
            return devices;
        }
        private static void PushDeviceToDevices(Device[,] devices, char[] rowdevices, List<string> loadedparameters, int i, int j, ref int parameterindex)
        {
            Device device;
            if (!Device.IsComplexCharacter(rowdevices[j]))
            {
                device = Device.GetDeviceByCharacter(rowdevices[j]);
            }
            else
            {
                try
                {
                    device = Device.GetDeviceByCharacter(rowdevices[j], loadedparameters[parameterindex]);
                }
                catch (InvalidCastException)
                {
                    // Door's row, columns separated by
                    string[] positions = loadedparameters[parameterindex].Split(' ');
                    int row = Convert.ToInt32(positions[0]);
                    int col = Convert.ToInt32(positions[1]);
                    devices[row - 1, col - 1] = new Door();
                    device = Device.GetDeviceByCharacter(rowdevices[j], devices[row - 1, col - 1]);
                }
                parameterindex++;
            }
            devices[i, j] = device;
        }
        private static List<string> ReadLines(string filename)
        {
            if (assembly.GetManifestResourceNames().Contains(filename))
            {
                System.IO.Stream stream = assembly.GetManifestResourceStream(filename);
                System.IO.StreamReader str = new System.IO.StreamReader(stream);
                List<string> lines = new List<string>();
                string line = "";
                do
                {
                    line = str.ReadLine();
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
                throw new System.IO.FileNotFoundException();
            }
        }
        private static Path ReadPath(string preview, string filename, int level, string extension, bool example)
        {
            string examplepathfilename = filename + extension;
            string exampleparametersfilename = examplepathfilename + ".params";
            List<string> loadeddevices = null;
            List<string> loadedparams = null;
            Device[,] devices;
            try
            {
                loadeddevices = ReadLines(examplepathfilename);
                try
                {
                    loadedparams = ReadLines(exampleparametersfilename);
                    devices = GetDevices(loadeddevices, loadedparams);
                }
                catch (System.IO.FileNotFoundException)
                {
                    devices = GetDevices(loadeddevices);
                }
                return new Path(example, devices, level, preview);
            }
            catch (System.IO.FileNotFoundException)
            {
                return null;
            }
        }

        #endregion Private Methods
    }
}
