using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Sinux
{
    public sealed class Logfile
    {
        private static readonly string logdir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        private static string logfile = string.Empty;
        public static readonly Logfile Instance = new Logfile();

        internal Logfile()
        {
            if (Directory.Exists(logdir) == false)
            {
                Directory.CreateDirectory(logdir);
            }
            string logfilename = String.Format("log{0:000}.txt", GetNextLogfileNumber());
            logfile = Path.Combine(logdir, logfilename);
            Console.WriteLine("Logfile: {0}", logfile);
        }

        public void Write(string s)
        {
            File.AppendAllText(logfile, s + Environment.NewLine);
        }

        private int GetNextLogfileNumber()
        {
            Regex rg = new Regex(@"log(\d{3})\.txt$");
            int max_number = 0;

            foreach (string name in Directory.GetFiles(logdir))
            {
                Match match = rg.Match(name);
                if (match.Success && match.Groups.Count > 0)
                {
                    max_number = Math.Max(max_number, int.Parse(match.Groups[1].Value));
                }
            }
            return max_number + 1;
        }
    }
}
