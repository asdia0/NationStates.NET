namespace NationStates.NET.Nation
{
    using System;

    public class Event
    {
        public DateTime TimeStamp { get; set; }

        public string Text { get; set; }

        public Event(DateTime timestamp, string text)
        {
            this.TimeStamp = timestamp;
            this.Text = text;
        }
    }
}
