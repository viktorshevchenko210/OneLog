using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneLog.Models;
using System.IO;
using System.Text;

namespace OneLog.Infrastructure
{
    internal sealed class File : IFile
    {
        private readonly TextWriter output;
        private readonly bool isBuffered;

        public File(string path, bool isBuffered=false)
        {
            this.isBuffered = isBuffered;

            Stream outputStream = System.IO.File.Open(path, FileMode.Append, FileAccess.Write, FileShare.Read);
            output = new StreamWriter(outputStream, Encoding.UTF8);
        }

        public void Write(Request request)
        {
            output.WriteLine(JsonConvert.SerializeObject(request, new StringEnumConverter()));
            if (!isBuffered)
                output.Flush();
        }

        public void Dispose()
        {
            output.Flush();
            output.Dispose();
        }
    }
}
