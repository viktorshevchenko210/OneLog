using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OneLog.Models;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OneLog.Tests")]

namespace OneLog
{
    internal class Log : OneLog.ILogger, Microsoft.Extensions.Logging.ILogger
    {
        public Request Request { get; }

        public Log(IHttpContextAccessor accessor)
        {
            Request = new Request(accessor.HttpContext?.Request.Path.Value); 
        }

        public void LogEvent(string name, string value, EventCategory category)
        {
            Request.AddEvent(name, value, category);
        }

        public void LogException(Exception exception)
        {
            Request.AddException(exception);
        }

        void Microsoft.Extensions.Logging.ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
