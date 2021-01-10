using Microsoft.AspNetCore.Http;
using OneLog.Infrastructure;
using OneLog.Models;
using System;
using System.Threading.Tasks;

namespace OneLog.Middleware
{
    internal class SaveLogMiddleware
    {
        private readonly RequestDelegate _next;
        public SaveLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger log, IFile file)
        {
            try
            {
                await _next.Invoke(context);
                WriteRequest(context, log, file);
            }
            catch (Exception ex)
            {
                log.LogException(ex);
                WriteRequest(context, log, file);
                throw;
            }
        }

        private void WriteRequest(HttpContext context, ILogger log, IFile file)
        {
            Guid requestId = TraceId(context);

            if (log.Requests.TryRemove(requestId, out Request request))
            {
                file.Write(request);
            }
        }

        private Guid TraceId(HttpContext context)
        {
            return (Guid)(context.Items["TraceIdentifier"] ?? Guid.Empty);
        }
    }
}
