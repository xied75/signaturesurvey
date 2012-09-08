using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SurveyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var path = args[1];
            string path = "../../Program.cs";
            var chars = new List<char> {'{', '}', ';'}.ToList();
            using (FileStream file = File.OpenRead(path))
            {
                for (int i = 0; i < file.Length; i++)
                {
                    char c = (char) file.ReadByte();
                    if (chars.Contains(c))
                        Console.Write(c);
                }
            }
            Console.ReadKey();
        }
    }
}