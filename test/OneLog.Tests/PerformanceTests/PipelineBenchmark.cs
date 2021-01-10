using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OneLog;
using OneLog.Infrastructure;
using OneLog.Models;
using OneLog.Options;
using System;

namespace Tests.Benchmarks
{
    /// <summary>
    /// Tests the cost of writing through the logging pipeline.
    /// </summary>
    [MemoryDiagnoser]
    public class PipelineBenchmark
    {
        private IFile file;

        [GlobalSetup]
        public void Setup()
        {
            var services = new ServiceCollection()
                   .AddOptions()
                   .Configure<LoggerOptions>(q =>
                   {
                       q.FileName = $"Requests";
                       q.LoggerFolder = @"C:\Users\Shevchenko\source\repos\OneLog\Logs\";
                       q.IsBuffered = true;
                   })
                   .AddSingleton<IClock, Clock>(serviceProvider => 
                   {
                       return new Clock(DateTime.Now);
                   })
                   .AddSingleton<ClockCheckerFileDecorator>();

            var sp = services.BuildServiceProvider();

            file = sp.GetRequiredService<ClockCheckerFileDecorator>();
        }

        private ILogger CreateLogger()
        {
            var accessor = new HttpContextAccessor();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/api/test";

            accessor.HttpContext = httpContext;

            return new Logger(accessor);
        }

        [Benchmark]
        public void LogWithException()
        {
            var logger = CreateLogger();

            for (int j = 0; j < 5; j++)
            {
                logger.LogEvent("TEST_EVENT", "TEST_EVENT", EventCategory.Information);
            }

            try
            {
                throw new Exception("TEST_EXCEPTION");
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }

            file.Write(logger.Request);
        }

        [Benchmark]
        public void LogWithoutException()
        {
            var logger = CreateLogger();

            for (int j = 0; j < 5; j++)
            {
                logger.LogEvent("TEST_EVENT", "TEST_EVENT", EventCategory.Information);
            }

            file.Write(logger.Request);
        }
    }
}
