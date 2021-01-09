using Microsoft.AspNetCore.Http;
using OneLog.Models;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OneLog.Tests")]

namespace OneLog
{
    internal class Log : ILogger
    {
        public Request Request { get; }

        public Log(IHttpContextAccessor accessor)
        {
            Request = new Request(accessor.HttpContext.Request?.Path.Value); 
        }

        public void LogEvent(string name, string value, EventCategory category)
        {
            Request.AddEvent(name, value, category);
        }

        public void LogException(Exception exception)
        {
            Request.AddException(exception);
        }
    }
}
