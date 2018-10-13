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
            var cfg = new {
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
            catch (Exception ex)
            {
                DirectoryInfo diretoriolog = new DirectoryInfo(DefaultLogFilePath);
                if (!diretoriolog.Exists)
                {
                    diretoriolog.Create();
                }
                using (StreamWriter sw = new StreamWriter(diretoriolog + "\\" + "LogError_" + Process.GetCurrentProcess().ProcessName + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log"))
                {
                    sw.WriteLine();
                    sw.WriteLine("-------" + DateTime.Now + "-------");
                    sw.WriteLine("----------" + local + "----------");
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
        }
        public static readonly string DefaultLogFilePath = System.IO.Path.GetTempPath();
        // Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
}
