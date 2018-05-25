using System.Runtime.InteropServices;

namespace GetToken
{
    public static class Clipboard
    {
        public static void Copy(string text)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Shell.Bat($"echo {text} | clip");
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Shell.Bash($"echo \"{text}\" | pbcopy");
            }
        }
    }
}
