namespace NationStates.NET
{
    using System;
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a notice.
    /// </summary>
    public struct Notice
    {
        /// <summary>
        /// Gets a value indicating whether the notice is new.
        /// </summary>
        [JsonProperty]
        public bool New { get; }

        /// <summary>
        /// Gets the notice's text.
        /// </summary>
        [JsonProperty]
        public string Text { get; }

        /// <summary>
        /// Gets the time at which the notice was sent.
        /// </summary>
        [JsonProperty]
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the notice's title.
        /// </summary>
        [JsonProperty]
        public string Title { get; }

        /// <summary>
        /// Gets the notice's type.
        /// </summary>
        [JsonProperty]
        public NoticeType Type { get; }

        /// <summary>
        /// Gets the notice's URL.
        /// </summary>
        [JsonProperty]
        public string URL { get; }

        /// <summary>
        /// Gets the subject of the notice.
        /// </summary>
        [JsonProperty]
        public string Who { get; }

        /// <summary>
        /// Gets the URL to the subject of the notice.
        /// </summary>
        [JsonProperty]
        public string WhoURL { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notice"/> struct.
        /// </summary>
        /// <param name="title">The notice's title.</param>
        /// <param name="type">The notice's type.</param>
        /// <param name="text">The notice's text.</param>
        /// <param name="url">The notice's URL.</param>
        /// <param name="timestamp">The time at which the notice was sent.</param>
        /// <param name="who">The subject of the notice.</param>
        /// <param name="whoURL">The URL to the subject of the notice.</param>
        /// <param name="isNew">A value indicating whether the notice is new.</param>
        public Notice(string title, NoticeType type, string text, string url, DateTime timestamp, string who, string whoURL, bool isNew)
        {
            this.Title = title;
            this.Type = type;
            this.Text = text;
            this.URL = url;
            this.Timestamp = timestamp;
            this.Who = who;
            this.WhoURL = whoURL;
            this.New = isNew;
        }

        /// <summary>
        /// Gets a JSON string representing the notice.
        /// </summary>
        /// <returns>A JSON string representing the notice.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}