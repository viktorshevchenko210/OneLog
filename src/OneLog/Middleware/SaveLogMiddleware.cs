using Microsoft.AspNetCore.Http;
using OneLog.Infrastructure;
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
                file.Write(log.Request);
            }
            catch (Exception ex)
            {
                log.LogException(ex);
                file.Write(log.Request);
                throw;
            }
        }
    }
}
