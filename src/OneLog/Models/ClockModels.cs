using System;

namespace OneLog.Models
{
    internal interface IClock
    {
        DateTime Now { get; }
        bool Equals(DateTime dateTime);
    }

    internal class Clock : IClock
    {
        public DateTime Now { get; }

        public Clock(DateTime dateTime)
        {
            Now = dateTime;
        }

        public bool Equals(DateTime dateTime)
        {
            if (Now.Day != dateTime.Day)
                return false;

            return true;
        }
    }
}
