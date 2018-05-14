using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProcessArguments
{
    class Program
    {
        private const string _urls = "--urls http://127.0.0.1:0;https://127.0.0.1:0";

        static void Main(string[] args)
        {
            Console.WriteLine($"Process.GetCurrentProcess().Id: {Process.GetCurrentProcess().Id}");

            if (args.Length == 0)
            {
                RunProcess("dotnet", $"run --no-build {_urls}", GetProjectDir(Environment.CurrentDirectory));
            }
            else
            {
                for (var i = 0; i < args.Length; i++)
                {
                    Console.WriteLine($"args[{i}]: {args[i]}");
                }
            }
        }

        private static string GetProjectDir(string path)
        {
            if (Directory.GetFiles(path).Any(f => f.EndsWith(".csproj")))
            {
                return path;
            }
            else
            {
                return GetProjectDir(Path.Combine(path, ".."));
            }
        }

        private static void RunProcess(string filename, string arguments, string workingDirectory)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = filename,
                    Arguments = arguments,
                    //RedirectStandardInput = true,
                    //RedirectStandardOutput = true,
                    //RedirectStandardError = true,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory,
                }
            };

            process.Start();
            process.WaitForExit();
        }
    }
}
