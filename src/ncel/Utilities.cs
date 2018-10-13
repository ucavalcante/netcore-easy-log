using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace ncel
{
    public static class Utilities
    {
        public static string DestinationPath()
        {
            string sDiretorioLog = Directory.GetCurrentDirectory() + "\\Logs";
            //ToDo Create a config method
            var cfg = new
            {
                DiretorioLogs = Directory.GetCurrentDirectory() + "\\Logs"
            };
            DirectoryInfo log = new DirectoryInfo(cfg.DiretorioLogs);
            if (log.Exists)
            {
                sDiretorioLog = log.FullName;
            }
            return sDiretorioLog;
        }
        public static string CallStackExtraction(LogLevel level)
        {
            var StackSequence = "";
            var name = Assembly.GetExecutingAssembly().GetName();
            StackTrace stackTrace = new StackTrace(); // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();
            var previowsFrame = stackFrames[2].GetMethod().Name;
            for (int i = (stackFrames.Length - 1); i > 1; i--)
            {
                Console.WriteLine(stackFrames[i].GetMethod().Name); // write method name
                StackSequence = $"{StackSequence}{stackFrames[i].GetMethod().Name}().";
            }
            var local = "";
            StackSequence = StackSequence.Remove(StackSequence.Length - 1);
            switch (level)
            {
                case LogLevel.Information:
                    local = $"{Process.GetCurrentProcess().ProcessName}|{previowsFrame}";
                    break;

                default:
                    local = $"{Process.GetCurrentProcess().ProcessName}|{previowsFrame}|{StackSequence}";
                    break;
            }
            return local;
        }
        public static void StoreLineInFile(string sDiretorioLog, string line)
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
        public static void NCELFailToLog(string msgToLog, string local, Exception ex)
        {
            Console.WriteLine($"Fail to Log With current config, trying to log in this path{DefaultLogFilePath}");
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
        private static readonly string DefaultLogFilePath = $"{System.IO.Path.GetTempPath()}\\NCELFailToLog";
    }
}
