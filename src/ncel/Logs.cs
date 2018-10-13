using System;

namespace ncel
{
    class Logs
    {
        public static void Information(string msgToLogToLog)
        {
            string sDiretorioLog = Directory.GetCurrentDirectory() + "\\Logs";

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
                using (StreamWriter sw = File.AppendText(diretoriolog + "\\" + "Log_" + Process.GetCurrentProcess().ProcessName + "_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log"))
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
        public static readonly string DefaultLogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
}
