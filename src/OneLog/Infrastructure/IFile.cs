using OneLog.Models;
using System;

namespace OneLog.Infrastructure
{
    internal interface IFile : IDisposable
    {
        void Write(Request request);
    }
}
