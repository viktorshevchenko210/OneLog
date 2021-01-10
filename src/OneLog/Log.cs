using Microsoft.AspNetCore.Http;
using OneLog.Models;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OneLog.Tests")]

namespace OneLog
{
    internal class Log : OneLog.ILogger
    {
        private readonly IHttpContextAccessor accessor;
        public ConcurrentDictionary<Guid, Request> Requests { get; }

        public Log(IHttpContextAccessor accessor)
        {
            Requests = new ConcurrentDictionary<Guid, Request>();
            this.accessor = accessor;
        }

        public void LogEvent(string name, string value, EventCategory category)
        {
            if(!Requests.TryGetValue(TraceId, out Request request))        
                request = CreateRequest();

            request.AddEvent(name, value, category);
        }

        public void LogException(Exception exception)
        {
            if (!Requests.TryGetValue(TraceId, out Request request))
                request = CreateRequest();

            request.AddException(exception);
        }

        private Request CreateRequest()
        {
            return new Request(TraceId, accessor.HttpContext?.Request.Path.Value);
        }

        private Guid TraceId
        {
            get
            {
                return (Guid)(accessor.HttpContext?
                    .Items["TraceIdentifier"] ?? Guid.Empty);
            }
        }
    }
}
