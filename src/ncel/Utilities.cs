using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

            string previowsFrame = GetPreviousFrame(stackFrames);

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

        private static string GetPreviousFrame(StackFrame[] stackFrames)
        {
            Dictionary<string, int> ignoredMethods = new Dictionary<string, int>();

            ignoredMethods.Add("StoreLineInFile", 0);
            ignoredMethods.Add("CallStackExtraction", 0);
            ignoredMethods.Add("GetPreviousFrame", 0);

            ignoredMethods.Add("Emergency", 0);
            ignoredMethods.Add("Alert", 0);
            ignoredMethods.Add("Critical", 0);
            ignoredMethods.Add("Error", 0);
            ignoredMethods.Add("Warning", 0);
            ignoredMethods.Add("Notification", 0);
            ignoredMethods.Add("Information", 0);
            ignoredMethods.Add("Debug", 0);

            ignoredMethods.ToList().ForEach(e =>
            {
                ignoredMethods[e.Key] = stackFrames.ToList().FindIndex(sf => sf.GetMethod().Name == e.Key);
            });
            try
            {
                return stackFrames[ignoredMethods.Max(x => x.Value) > stackFrames.Length ? stackFrames.Length : ignoredMethods.Max(x => x.Value) + 1].GetMethod().Name;
            }
            catch (System.Exception ex)
            {
                return $">Error to get MethodName:{ex.Message}<";
            }
        }

        public static void NCELFailToLog(Exception ex, string msgToLog)
        {
            var id = DateTime.Now.Ticks;
            Console.WriteLine($"Fail to Log With current config, trying to log in this path:{DefaultLogFilePath}");
            DirectoryInfo diretoriolog = new DirectoryInfo(DefaultLogFilePath);
            if (!diretoriolog.Exists)
            {
                diretoriolog.Create();
            }
            var file = $"{diretoriolog}\\{System.AppDomain.CurrentDomain.FriendlyName}_{DateTime.Now.ToString("yyyy_MM_dd")}.log";
            using (StreamWriter sw = new StreamWriter(file))
            {
                var name = Assembly.GetExecutingAssembly().GetName();
                sw.WriteLine($"[{id}][{DateTime.Now}][Fail to record in]:'{DestinationPath()}'");
                sw.WriteLine($"[{id}][msgToLog]{msgToLog}");
                sw.WriteLine($"[{id}][Exception-Thrown]{ex.GetType()}[Message]{ex.Message}[Data]{ex.Data}");

                StackTrace stackTrace = new StackTrace(); // get call stack
                StackFrame[] stackFrames = stackTrace.GetFrames();
                var previowsFrame = stackFrames[1].GetMethod().Name;
                for (int i = (stackFrames.Length - 1); i >= 0; i--)
                {
                    var p = stackFrames[i].GetMethod().GetParameters();
                    var sp = "";
                    foreach (var item in p)
                    {
                        sp = $"{sp}{item.Name.ToString()},";
                    }
                    if (sp.EndsWith(","))
                    {
                        sp = sp.Remove(sp.Length - 1);
                    }


                    var msg = $"[{id}][pos:{i}][Class]{stackFrames[i].GetMethod().DeclaringType}[Method]{stackFrames[i].GetMethod().Name}({sp})";
                    sw.WriteLine(msg);
                }

            }
        }
        private static readonly string DefaultLogFilePath = $"{System.IO.Path.GetTempPath()}NCELFailToLog";
    }
}
