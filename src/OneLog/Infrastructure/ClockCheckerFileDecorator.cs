using Microsoft.Extensions.Options;
using OneLog.Models;
using OneLog.Options;
using System;

namespace OneLog.Infrastructure
{
    internal sealed class ClockCheckerFileDecorator : IFile
    {
        private IFile file;
        private IClock clock;
        private LoggerOptions options;

        private readonly object locker = new object();

        public ClockCheckerFileDecorator(IOptions<LoggerOptions> options, IClock clock)
        {
            if (options.Value == null) throw new ArgumentNullException(nameof(options.Value));

            if (string.IsNullOrEmpty(options.Value.FileName))
                throw new ArgumentNullException(nameof(options.Value.FileName));

            if (string.IsNullOrEmpty(options.Value.LoggerFolder))
                throw new ArgumentNullException(nameof(options.Value.LoggerFolder));

            this.options = options.Value;
            this.clock = clock;

            file = CreateFile(this.options);
        }

        public void Write(Request request)
        {
            lock (locker)
            {
                if (!clock.Equals(DateTime.Now))
                {
                    Dispose();
                    clock = new Clock(DateTime.Now);
                    file = CreateFile(options);
                }

                file.Write(request);
            }
        }

        private File CreateFile(LoggerOptions options)
        {
            string filePath = $"{options.LoggerFolder}{options.FileName}_{clock.Now:yyyy-MM-dd}.log";
            return new File(filePath, options.IsBuffered);
        }

        public void Dispose()
        {
            file.Dispose();
        }
    }
}
