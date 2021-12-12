namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents the unread stuff a nation has.
    /// </summary>
    public struct Unread
    {
        /// <summary>
        /// Gets the number of unread issues.
        /// </summary>
        [JsonProperty]
        public int Issues { get; }

        /// <summary>
        /// Gets the number of unread messages.
        /// </summary>
        [JsonProperty]
        public int Messages { get; }

        /// <summary>
        /// Gets the number of unread news articles.
        /// </summary>
        [JsonProperty]
        public int News { get; }

        /// <summary>
        /// Gets the number of unread notices.
        /// </summary>
        [JsonProperty]
        public int Notices { get; }

        /// <summary>
        /// Gets the number of unread telegrams.
        /// </summary>
        [JsonProperty]
        public int Telegrams { get; }

        /// <summary>
        /// Gets the number of World Assembly proposals to vote on.
        /// </summary>
        [JsonProperty]
        public int WorldAssembly { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unread"/> struct.
        /// </summary>
        /// <param name="issues">The number of unread issues.</param>
        /// <param name="telegrams">The number of unread telegrams.</param>
        /// <param name="notices">The number of unread notices.</param>
        /// <param name="messages">The number of unread messages.</param>
        /// <param name="worldAssembly">The number of World Assembly proposals to vote on.</param>
        /// <param name="news">The number of unread news articles.</param>
        public Unread(int issues, int telegrams, int notices, int messages, int worldAssembly, int news)
        {
            this.Issues = issues;
            this.Telegrams = telegrams;
            this.Notices = notices;
            this.Messages = messages;
            this.WorldAssembly = worldAssembly;
            this.News = news;
        }

        /// <summary>
        /// Gets a JSON string representing the unread stuff.
        /// </summary>
        /// <returns>A JSON string representing the unread stuff.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}