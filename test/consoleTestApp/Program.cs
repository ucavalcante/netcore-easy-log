using System;
using ncel;

namespace consoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string msgToLog = "Hello World!";
            Console.WriteLine(msgToLog);
            Log.Information(msgToLog);
            Metodo1("Chamando Metodo1");
        }
        public static void Metodo1(string msg)
        {
            Log.Information(msg);
            Metodo2("Chamando metodo2");
        }

        public static void Metodo2(string msg)
        {
            Log.Information(msg);
        }
    }
}
