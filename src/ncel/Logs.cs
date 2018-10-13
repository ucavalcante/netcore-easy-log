using System;
using System.Diagnostics;
using System.IO;

namespace ncel
{
    class Logs
    {
        public static void Information(string msgToLog)
        {
            string sDiretorioLog = Directory.GetCurrentDirectory() + "\\Logs";
            //ToDo Create a config method
            var cfg = new
            {
                DiretorioLogs = Directory.GetCurrentDirectory() + "\\Logs"
            };
            //ToDo Create a way to Get Previous caller Method
            var local = "InConstruction";
            DirectoryInfo log = new DirectoryInfo(cfg.DiretorioLogs);
            if (log.Exists)
            {
                sDiretorioLog = log.FullName;
            }
            try
            {
                var line = $"[{DateTime.Now}][INFO][{local}][{msgToLog}]";
                StoreLineInFile(sDiretorioLog, line);
            }
            catch (Exception ex)
            {
                NCELFailToLog(msgToLog, local, ex);
                Console.WriteLine($"Fail to Log With current config, trying to log in this path{DefaultLogFilePath}");
            }
        }
        private static void StoreLineInFile(string sDiretorioLog, string line)
        {
            DirectoryInfo diretoriolog = new DirectoryInfo(sDiretorioLog);
            if (!diretoriolog.Exists)
            {
                diretoriolog.Create();
            }
            using (StreamWriter sw = File.AppendText($"{diretoriolog}\\Log_{Process.GetCurrentProcess().ProcessName}_{DateTime.Now.ToString("yyyy_MM_dd")}.log"))
            {
                sw.WriteLine(line);
            }
        }
        private static void NCELFailToLog(string msgToLog, string local, Exception ex)
        {
            DirectoryInfo diretoriolog = new DirectoryInfo(DefaultLogFilePath);
            if (!diretoriolog.Exists)
            {
                diretoriolog.Create();
            }
            using (StreamWriter sw = new StreamWriter(diretoriolog + "\\" + "LogError_" + Process.GetCurrentProcess().ProcessName + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log"))
            {
                sw.WriteLine();
                sw.WriteLine($"-------{DateTime.Now}-------");
                sw.WriteLine($"----------{local}----------");
                sw.WriteLine();
                sw.WriteLine(msgToLog);
                sw.WriteLine();
                sw.WriteLine("--------------------------------");
                sw.WriteLine();
                sw.WriteLine(ex.Message);
                sw.WriteLine();
                sw.WriteLine("--------------------------------");
                sw.WriteLine();
            }
        }
        public static readonly string DefaultLogFilePath = $"{System.IO.Path.GetTempPath()}\\NCELFailToLog";
        // Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
}