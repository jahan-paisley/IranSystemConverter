using System;
using System.IO;

namespace IranSystemConvertor
{
    public static class Logger
    {
        public static string Path;  
        public static TextWriter Writer;

        public static void Init(string path)
        {
            var newpath= (path ?? "log") + "_" + DateTime.Now.ToString("O").Replace(":", "-") + ".log";
            Writer= new StreamWriter(newpath);
        }

        public static void Log(string input)
        {
            Writer.WriteLine(input);
        }
    }
}