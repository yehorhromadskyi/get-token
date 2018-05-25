using System.Diagnostics;

namespace GetToken
{
    public static class Shell
    {
        public static string Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var result = RunProcess("/bin/bash", $"-c \"{escapedArgs}\"");

            return result;
        }

        public static string Bat(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var result = RunProcess("cmd.exe", $"/c \"{escapedArgs}\"");

            return result;
        }

        private static string RunProcess(string filename, string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = filename,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                }
            };

            process.Start();

            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return result;
        }
    }
}
