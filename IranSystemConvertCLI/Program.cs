using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IranSystemConvertor;

namespace IranSystemConvertCLI
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Number of parameters is not correct, please provide an input file name");
                return -1;
            }
            var fileName = args[1];
            var newfileName = fileName + ".is";
            Logger.Init(fileName);
            var columns = args[0];
            var reader = new FileStream(fileName, FileMode.Open);
            var writer = new FileStream(newfileName, FileMode.Create);
            try
            {
                while (reader.CanRead)
                {
                    var line = new List<byte>();
                    int readByte;
                    bool b;
                    do
                    {
                        readByte = reader.ReadByte();
                        b = readByte != 13 && readByte != -1 && readByte != 10 && readByte != 255;
                        if (b)
                            line.Add((byte)readByte);
                    } while (b);
                    var arabicToIranSys = Arabic1256ToIranSystem.ArabicToIranSys(line.ToArray(), columns).ToArray();
                    if (arabicToIranSys.Length > 0)
                    {
                        writer.Write(arabicToIranSys, 0, arabicToIranSys.Length);
                        writer.WriteByte(13);
                        writer.WriteByte(10);
                    }
                    line.Clear();
                    if (readByte == -1)
                    {
                        writer.Flush();
                        writer.Close();
                        reader.Close();
                        Console.WriteLine("File has been written to " + newfileName);
                        return 0;
                    }
                }
            }
            finally
            {
                reader.Close();
                writer.Close();
                Logger.Writer.Close();
            }
            return -1;
        }
    }
}