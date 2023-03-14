using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Logging
{
    public class LoggingService : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }
}
