using CommandLine;
using CommandLine.Text;
using NuGet;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AssemblyInfoUtil
{
    /// <summary>
    /// Summary description for AssemblyVersionChanger.
    /// </summary>
    class AssemblyVersionChanger
    {
        class Options
        {
            [OptionArray('i', "input", Required = true, HelpText = "Input file(s) to read.")]
            public string[] InputFiles { get; set; }

            [Option("major", HelpText = "New Major version number.")]
            public string Major { get; set; }

            [Option("minor", HelpText = "New Minor version number.")]
            public string Minor { get; set; }

            [Option("revision", HelpText = "New Revision version number.")]
            public string Revision { get; set; }

            [Option("buid", HelpText = "New Build version number.")]
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
                        // http://msdn.microsoft.com/en-us/library/twcw2f1c(v=vs.110).aspx
                        Match m = reAssemblyVersion.Match(line);
                        if (!m.Success)
                        {
                            m = reAssemblyFileVersion.Match(line);
                        }

                        if (m.Success)
                        {
                            SemanticVersion v = SemanticVersion.Parse(m.Groups[1].Value);
                            string[] newVersion = new string[] {
                                options.Major ?? v.Version.Major.ToString(),
                                options.Minor ?? v.Version.Minor.ToString(),
                                options.Revision ?? v.Version.Revision.ToString(),
                                options.Build ?? v.Version.Build.ToString()
                            };

                            writer.WriteLine(
                                line.Substring(0, m.Groups[1].Index) +
                                String.Join(".",newVersion) +
                                line.Substring(m.Groups[1].Index + m.Groups[1].Length)
                            );
                        }
                        else
                        {
                            writer.WriteLine(line);
                        }
                    }
                    reader.Close();
                    writer.Close();

                    //File.Delete(file);
                    //File.Move(file + ".out", file);
                }
                System.Console.WriteLine("Done!");
            }
		}
    }
}
