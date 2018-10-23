using System;
using System.IO;
using System.Reflection;
using Galore.Services.Interfaces;

namespace Galore.Services.Implementations
{
    public class LogService : ILogService
    {
        public void LogToFile(string message)
        {
            Console.WriteLine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            using (StreamWriter sw = new StreamWriter("../exception_log.txt", true))
            {
                if (sw != null)
                {
                    sw.WriteLine($"{DateTime.Now} - {message}");
                }
            }
        }
    }
}
