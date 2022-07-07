using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IntegrationTests
{
    public class ProcessExec
    {
        public readonly string? output;

        public ProcessExec(List<string>? args)
        {
            using Process process = new Process();

            Configure(process, args);

            process.Start();

            output = process.StandardOutput.ReadLine();

            process.WaitForExit();
        }

        private void Configure(Process process, List<string>? args)
        {
            process.StartInfo.FileName = GetPath();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;

            if (args != null)
            {
                foreach (string arg in args)
                {
                    process.StartInfo.ArgumentList.Add(arg);
                }
            }
        }

        private string GetPath()
        {
            string? projectDir = Path.GetDirectoryName(
                                 Path.GetDirectoryName(
                                 Path.GetDirectoryName(
                                 Path.GetDirectoryName(
                                 Path.GetDirectoryName(
                                 Directory.GetCurrentDirectory())))));

            string pathToExe = @"SimpleCalculator\bin\Debug\net6.0\SimpleCalculator.exe";

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pathToExe = ConvertPath(pathToExe);
            }

            return Path.Join(projectDir, pathToExe);
        }

        private string ConvertPath(string pathToExe)
        {
            string convertedPath = pathToExe;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                convertedPath = Path.GetFileNameWithoutExtension(convertedPath);
            }
            else
            {
                throw new OSNotSupportedException("Cannot execute the program on this OS!");
            }

            return convertedPath.Replace('\\', Path.DirectorySeparatorChar);
        }

    }
}
