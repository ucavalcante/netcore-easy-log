namespace Ncel
{
    public static class Log
    {
        public static void Information(string msgToLog)
        {
            Recorder.StoreLineInFile(LogLevel.Information, msgToLog);
        }
        public static void Error(string msgToLog)
        {
            Recorder.StoreLineInFile(LogLevel.Error, msgToLog);
        }
    }
}