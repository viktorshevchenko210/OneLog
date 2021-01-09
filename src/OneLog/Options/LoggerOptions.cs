namespace OneLog.Options
{
    internal class LoggerOptions
    {
        public string LoggerFolder { get; set; }
        public string FileName { get; set; }
        public bool IsBuffered { get; set; } = false;
    }
}
