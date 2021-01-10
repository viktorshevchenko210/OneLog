using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace OneLog.Middleware
{
    internal class TraceIdMiddleware
    {
        private readonly RequestDelegate next;
        public TraceIdMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Items.Add("TraceIdentifier", Guid.NewGuid());
            await next.Invoke(context);
        }
    }
}
