using Newtonsoft.Json;


namespace OneLog.Models
{
    public class Event
    {
        [JsonProperty(Order = 0)]
        public string Name { get; private set; }

        [JsonProperty(Order = 1)]
        public string Value { get; private set; }

        [JsonProperty(Order = 2)]
        public EventCategory Category { get; private set; }

        public Event(string name, string value, EventCategory category)
        {
            this.Name = name;
            this.Value = value;
            this.Category = category;
        }
    }

    public enum EventCategory
    {
        Information,
        Warning,
        Error,
        Critical
    }
}
