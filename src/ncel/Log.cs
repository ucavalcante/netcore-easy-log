namespace ncel
{
    public static class Log
    {
        public static void Information(string msgToLog)
        {
            Recorder.StoreLineInFile(LogLevel.Information, msgToLog);
        }
    }
}