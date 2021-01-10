using OneLog.Models;
using System;
using System.Collections.Concurrent;

namespace OneLog
{
    public interface ILogger
    {
        ConcurrentDictionary<Guid, Request> Requests { get; }
        void LogEvent(string name, string value, EventCategory category);
        void LogException(Exception ex);
    }
}
