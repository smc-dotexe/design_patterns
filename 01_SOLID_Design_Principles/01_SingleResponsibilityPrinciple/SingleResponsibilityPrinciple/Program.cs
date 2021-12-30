using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SingleResponsibilityPrinciple
{
    // SRP - Every module/class/function should have a responsibility over a single part of that program's functionality
    // and it should encapsulate that part  https://en.wikipedia.org/wiki/Single-responsibility_principle
    
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // memento pattern
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index); 
            // Not a stable way to remove entries. Once removed,
            // the indices will change.
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    // This class handles a different responsibility than Journal keeping the
    // SRP consistent (separation of concerns)
    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I coded today");
            j.AddEntry("I worked out");

            var p = new Persistence();
            var filename = @"F:\webdev\tutorials\udemy\design_patterns_c_sharp\01_SOLID_Design_Principles\01_SingleResponsibilityPrinciple\srp_pattern.txt";
            p.SaveToFile(j, filename, true);


            // .NetCore version of Process.Start(filename)
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(filename)
            {
                UseShellExecute = true
            };
            process.Start();

        }
    }
}
