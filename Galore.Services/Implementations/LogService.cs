using System;
using System.IO;
using System.Reflection;
using Galore.Services.Interfaces;

namespace Galore.Services.Implementations
{
    /**
        LogService.cs
        used to log exceptions to a file
     */
    public class LogService : ILogService
    {
        public void LogToFile(string message)
        {
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
