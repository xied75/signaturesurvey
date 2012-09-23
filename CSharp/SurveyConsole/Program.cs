﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SurveyConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var path = args[0];
            var chars = new List<char> {'{', '}', ';'}.ToList();

            var loc = File.ReadAllLines(path).Length;
            Console.Write("{0} ({1}): ", Path.GetFileName(path), loc);

            using (var file = File.OpenRead(path))
            {
                for (var i = 0; i < file.Length; i++)
                {
                    var c = (char) file.ReadByte();
                    if (chars.Contains(c))
                        Console.Write(c);
                }
            }
        }
    }
}