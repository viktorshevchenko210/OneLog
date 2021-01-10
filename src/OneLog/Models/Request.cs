using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OneLog.Models
{
    public class Request
    {
        [JsonProperty(Order = 0)]
        public Guid Id { get; private set; }

        [JsonProperty(Order = 1)]
        public string Url { get; private set; }

        [JsonProperty(Order = 2)]
        public List<Event> Events { get; private set; } = new List<Event>();

        [JsonProperty(Order = 3)]
        public List<CustomException> Exceptions { get; private set; } = new List<CustomException>();

        [JsonProperty(Order = 4)]
        public string Timestamp { get; private set; }

        public Request(Guid id, string url)
        {
            this.Id = id;
            this.Url = url;
            this.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
        }

        internal void AddEvent(string name, string value, EventCategory category)
        {
            Event @event = new Event(name, value, category);
            Events.Add(@event);
        }

        internal void AddException(Exception exception)
        {
            Exceptions.Add(new CustomException(exception));
        }
    }
}
