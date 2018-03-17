using CommandLine;

namespace PJE.WatchOrders
{
    /// <summary>
    /// Class representing the command line options that this program
    /// accepts.  See https://github.com/commandlineparser/commandline for
    /// further details.
    /// </summary>
    public class Options
    {
        [Option('i', "input",
            HelpText = "Relative path to folder to watch for JSON input.",
            Required = true)]
        public string InputDirectory { get; set; }

        [Option('o', "output",
            HelpText = "Relative path to folder to place CSV output.",
            Required = true)]
        public string OutputDirectory { get; set; }

        [Option('r', "recursive",
            HelpText = "Recurse into subfolders of the input folder.",
            Required = false,
            Default = false)]
        public bool Recursive { get; set; }
    }
}
