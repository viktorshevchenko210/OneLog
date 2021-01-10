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
            Guid traceId = TraceId;

            Request request = CreateRequest(traceId);
            request.AddEvent(name, value, category);

            Requests[traceId] = request;
        }

        public void LogException(Exception exception)
        {
            Guid traceId = TraceId;

            Request request = CreateRequest(traceId);
            request.AddException(exception);

            Requests[traceId] = request;
        }

        private Request CreateRequest(Guid traceId)
        {
            if (!Requests.TryGetValue(traceId, out Request request))
                request = new Request(traceId, accessor.HttpContext?.Request.Path.Value);

            return request;
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
