using System;
using System.Collections.Generic;

namespace ConsoleMario.Utility
{
    internal static class CheckedDataInput
    {
        private const int SLEEPSECONDS = 1000;

        // return truestring == Input()
        public static bool DecisionInput(string inputstring, string truestring)
        {
            string data = Input(inputstring);
            Console.Clear();
            return (data == truestring);
        }
        // Write the inputstring and Read the data without check or converstion
        public static string Input(string inputstring = "Input the expected data!")
        {
            string data = "";
            Console.WriteLine(inputstring);
            data = Console.ReadLine();
            Console.Clear();
            return data;
        }
        public static T InputChar<T>(string inputstring = "Input the expected data!")
        {
            Console.WriteLine(inputstring);
            T ch = (T)Convert.ChangeType(Console.ReadKey().Key, typeof(T));
            Console.WriteLine("Given data: {0}", ch);
            System.Threading.Thread.Sleep(SLEEPSECONDS);
            Console.Clear();
            return ch;
        }
    }
}
