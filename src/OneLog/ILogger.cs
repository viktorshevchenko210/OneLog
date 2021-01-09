using OneLog.Models;
using System;

namespace OneLog
{
    public interface ILogger
    {
        Request Request { get; }
        void LogEvent(string name, string value, EventCategory category);
        void LogException(Exception ex);
    }
}
