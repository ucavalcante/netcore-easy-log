using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Ncel
{
    public static class Utilities
    {
        public static string DestinationPath()
        {
            DirectoryInfo WorkDir = new DirectoryInfo(LogConfig.DirectoryPath);
            if (!WorkDir.Exists)
            {
                WorkDir.Create();
            }
            return $"{WorkDir}\\{System.AppDomain.CurrentDomain.FriendlyName}_{DateTime.Now.ToString("yyyy_MM_dd")}.log";
        }
        public static string CallStackExtraction(LogLevel level)
        {
            var StackSequence = "";
            var name = Assembly.GetExecutingAssembly().GetName();
            StackTrace stackTrace = new StackTrace(); // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();
            var previowsFrame = stackFrames[3].GetMethod().Name;
            for (int i = (stackFrames.Length - 1); i > 1; i--)
            {
                StackSequence = $"{StackSequence}{stackFrames[i].GetMethod().Name}().";
            }
            var local = "";
            StackSequence = StackSequence.Remove(StackSequence.Length - 1);
            switch (level)
            {
                case LogLevel.Information:
                    local = $"{System.AppDomain.CurrentDomain.FriendlyName}|{previowsFrame}";
                    break;
                case LogLevel.Error:
                    local = $"{System.AppDomain.CurrentDomain.FriendlyName}|{previowsFrame}";
                    break;
                default:
                    local = $"{System.AppDomain.CurrentDomain.FriendlyName}|{previowsFrame}|{StackSequence}";
                    break;
            }
            return local;
        }


        public static void NCELFailToLog(Exception ex, string msgToLog)
        {
            Console.WriteLine($"Fail to Log With current config, trying to log in this path{DefaultLogFilePath}");
            DirectoryInfo diretoriolog = new DirectoryInfo(DefaultLogFilePath);
            if (!diretoriolog.Exists)
            {
                diretoriolog.Create();
            }
            var file = $"{diretoriolog}\\{System.AppDomain.CurrentDomain.FriendlyName}_{DateTime.Now.ToString("yyyy_MM_dd")}.log";
            using (StreamWriter sw = new StreamWriter(file))
            {
                sw.WriteLine();
                sw.WriteLine($"-------{DateTime.Now}-------");
                sw.WriteLine($"---Fail to record in:{DestinationPath()}");
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
