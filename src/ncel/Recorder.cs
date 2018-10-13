using System;
using System.IO;

namespace ncel
{
    public class Recorder
    {
        public static void StoreLineInFile(LogLevel level, string msgToLog)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(Utilities.DestinationPath()))
                {
                    sw.WriteLine($"[{DateTime.Now}][{level.ToString()}][{Utilities.CallStackExtraction(level)}]{msgToLog}");
                }
            }
            catch (Exception ex)
            {
                Utilities.NCELFailToLog(ex, msgToLog);
            }
        }
    }
}