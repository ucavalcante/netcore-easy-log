using System.IO;

namespace Ncel
{
    public static class LogConfig
    {
        public static bool WriteConsole { get; set; } = false;
        public static string DirectoryPath { get; set; } = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}Logs";
    }
}