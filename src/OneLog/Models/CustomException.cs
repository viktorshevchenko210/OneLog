using System;
using System.Collections.Generic;

namespace OneLog.Models
{
    public class CustomException
    {
        public string Message { get; private set; }
        public string StackTrace { get; private set; }
        public List<CustomException> InnerExceptions { get; private set; } = new List<CustomException>();

        public CustomException(Exception exception)
        {
            Message = exception.Message;
            StackTrace = exception.StackTrace;
            while(exception.InnerException != null)
            {
                InnerExceptions.Add(new CustomException(exception.InnerException));
                exception = exception.InnerException;
            }
        }
    }
}
