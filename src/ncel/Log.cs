using System;

namespace Ncel
{
    public static class Log
    {
        public static void Information(string msgToLog)
        {
            var level = LogLevel.Information;
            Recorder.StoreLineInFile(level, msgToLog);
            if (LogConfig.WriteConsole)
            {
                Console.WriteLine($"{level}:{msgToLog}");
            }
        }
    }
}