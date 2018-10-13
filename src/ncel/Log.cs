using System;


namespace ncel
{
    public static class Log
    {
        public static void Information(string msgToLog)
        {
            var level = LogLevel.Information;
            try
            {
                var line = $"[{DateTime.Now}][{level.ToString()}][{Utilities.CallStackExtraction(level)}|'{msgToLog}']";
                Utilities.StoreLineInFile(Utilities.DestinationPath(), line);
            }
            catch (Exception ex)
            {
                Utilities.NCELFailToLog(msgToLog, Utilities.CallStackExtraction(level), ex);
                
            }
        }
        
    }
}