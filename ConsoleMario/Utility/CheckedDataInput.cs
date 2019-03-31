﻿using System;
using System.Collections.Generic;

namespace ConsoleMario
{
    internal static class CheckedDataInput
    {
        private const int waitseconds = 700;
        #region Public Methods
        public static ConsoleKey InputChar(string inputstring = "Input the expected data!")
        {
            Console.WriteLine(inputstring);
            ConsoleKey ch = Console.ReadKey().Key;
            Console.WriteLine("Given data: {0}", ch);
            System.Threading.Thread.Sleep(1000);
            return ch;
        }
        // Write the inputstring and Read the data without check or converstion
        public static string Input(string inputstring = "Input the expected data!")
        {
            string data = "";
            Console.WriteLine(inputstring);
            data = Console.ReadLine();
            return data;
        }
        // return truestring == Input()
        public static bool DecisionInput(string inputstring, string truestring)
        {
            string data = Input(inputstring);
            Console.WriteLine("Given data: {0}", data == truestring);
            System.Threading.Thread.Sleep(1000);
            return (data == truestring);
        }
        // return T type with conversion
        public static T CheckDataInput<T>(string inputstring = "Input the expected data!")
        {
            string data = Input(inputstring);
            T converteddata = default(T);
            if (data == "")
            {
                bool datastaynull = DecisionInput("Are you sure, that you dont want to give any kind of data?\n If you dont want to give any kind of data, press Enter", "");
                if (!datastaynull)
                {
                    converteddata = CheckDataInput<T>(inputstring);
                }
            }
            else if (data != "")
            {
                try
                {
                    converteddata = (T)Convert.ChangeType(data, typeof(T));
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Error!");
                    Console.WriteLine(e.Message);
                    converteddata = CheckDataInput<T>(inputstring);
                }
            }
            Console.WriteLine("Given data: {0}", converteddata);
            System.Threading.Thread.Sleep(1000);
            return converteddata;
        }
        // return CheckDataInput and check if tombben is true then if data is in array return data, else call again
        public static T CheckDataInput<T>(List<T> array, string inputstring = "Input the expected data!", bool dataisinarray = true)
        {
            T converteddata = converteddata = CheckDataInput<T>(inputstring);
            if ((array.Contains(converteddata) && !dataisinarray) ||
                (!array.Contains(converteddata) && dataisinarray))
            {
                Console.WriteLine("Theres is no data like the give in the array!\n");
                converteddata = CheckDataInput(array, inputstring, dataisinarray);
            }
            Console.WriteLine("Given data: {0}", converteddata);
            return converteddata;
        }

        #endregion Public Methods
    }
}