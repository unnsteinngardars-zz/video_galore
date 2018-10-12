using System;
using System.IO;
using Galore.Services.Interfaces;

namespace Galore.Services.Implementations
{
    public class LogService : ILogService
    {
        public void LogToFile(string message)
        {
            using (var file = new StreamWriter("log.txt", true))
            {
                file.WriteLine($"{DateTime.Now} - {message}");
            }        
        }
    }
}
