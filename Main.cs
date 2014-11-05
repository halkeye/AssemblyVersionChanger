using CommandLine;
using CommandLine.Text;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AssemblyInfoUtil
{
    /// <summary>
    /// Summary description for AssemblyVersionChanger.
    /// </summary>
    public class AssemblyVersionChanger
    {
        public class Options
        {
            [OptionArray('i', "input", Required = true, HelpText = "Input file(s) to read.")]
            public string[] InputFiles { get; set; }

            [Option("major", HelpText = "New Major version number.")]
            public string Major { get; set; }

            [Option("minor", HelpText = "New Minor version number.")]
            public string Minor { get; set; }

            [Option("revision", HelpText = "New Revision version number.")]
            public string Revision { get; set; }

            [Option("build", HelpText = "New Build version number.")]
            public string Build { get; set; }

            [Option("version", HelpText = "Set version number")]
            public string Version { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }

        }

        private static Regex reAssemblyVersion = new Regex(@"[<\[]assembly:\s*AssemblyVersion\(\s*""([0-9\.]+)""\s*\)[\]>]", RegexOptions.IgnoreCase);
        private static Regex reAssemblyFileVersion = new Regex(@"[<\[]assembly:\s*AssemblyFileVersion\(\s*""([0-9\.]+)""\s*\)[\]>]", RegexOptions.IgnoreCase);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
		{
            var options = new Options();

            if (CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
            {
                foreach (string file in options.InputFiles)
                {
                    if ( !File.Exists(file) )
                    {
                        System.Console.WriteLine("Error: Can not find file \"" + file + "\"");
                        return;
                    }

                    System.Console.Write("Processing \"" + file + "\"...");
                    StreamReader reader = new StreamReader(file);
                    StreamWriter writer = new StreamWriter(file + ".out");
                    String line;

                    while ((line = reader.ReadLine()) != null) {
                        line = processLine(options, line);
                        writer.WriteLine(line);
                    }
                    reader.Close();
                    writer.Close();

                    File.Delete(file);
                    File.Move(file + ".out", file);
                }
                System.Console.WriteLine("Done!");
            }
		}

        public static string processLine(Options options, string line)
        {
            // http://msdn.microsoft.com/en-us/library/twcw2f1c(v=vs.110).aspx
            Match m = reAssemblyVersion.Match(line);
            if (!m.Success) { m = reAssemblyFileVersion.Match(line); }
            if (m.Success)
            {
                string[] oldVersion = m.Groups[1].Value.Split('.');
                if (!string.IsNullOrEmpty(options.Build) && oldVersion.Length >= 4)
                {
                    oldVersion[3] = options.Build;
                }
                if (!string.IsNullOrEmpty(options.Revision) && oldVersion.Length >= 3)
                {
                    oldVersion[2] = options.Revision;
                }
                if (!string.IsNullOrEmpty(options.Minor) && oldVersion.Length >= 2)
                {
                    oldVersion[1] = options.Minor;
                }
                if (!string.IsNullOrEmpty(options.Major) && oldVersion.Length >= 1)
                {
                    oldVersion[0] = options.Major;
                }

                return line.Substring(0, m.Groups[1].Index) +
                    String.Join(".", oldVersion) +
                    line.Substring(m.Groups[1].Index + m.Groups[1].Length);
            }
            return line;
        }
    }
}
