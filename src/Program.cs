using System;
using System.IO;
using System.Threading.Tasks;
using CommandLine;
using PJE.WatchOrders.Data;

namespace PJE.WatchOrders
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(Run);
        }

        private static void Run(Options options)
        {
            if (!Directory.Exists(options.InputDirectory))
            {
                Console.WriteLine($"{options.InputDirectory}: no such directory");
                return;
            }

            var manager = new Manager(
                options,
                new MemoryRepository(),
                new FolderInput(options.InputDirectory, options.Recursive));
            manager.RunForever();
        }
    }
}
