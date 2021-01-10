using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using OneLog.Infrastructure;
using OneLog.Middleware;
using OneLog.Models;
using OneLog.Options;
using System;

namespace OneLog.Extensions
{
    public static class LoggerExtensions
    {
        public static IServiceCollection AddOneLog(this IServiceCollection services, IConfiguration configuration)
        {
            if(configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.Configure<LoggerOptions>(configuration.GetSection("OneLog"));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogger, Log>();

            services.AddSingleton<IClock, Clock>(serviceProvider =>
            {
                return new Clock(DateTime.Now);
            });
            services.AddSingleton<IFile, ClockCheckerFileDecorator>();

            services.AddSingleton<ApplicationLifetime>();

            return services;
        }

        public static IApplicationBuilder UseOneLog(this IApplicationBuilder app)
        {
            app.UseMiddleware<SaveLogMiddleware>();
            app.UseMiddleware<TraceIdMiddleware>();
            return app;
        }
    }
}
