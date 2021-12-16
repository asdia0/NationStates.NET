namespace NationStates.NET
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a telegram client.
    /// </summary>
    public struct Telegram
    {
        /// <summary>
        /// Gets the user's client key.
        /// </summary>
        [JsonProperty]
        public string ClientKey { get; }

        /// <summary>
        /// Gets the recipients of the telegram in Auralia's <see href="https://github.com/auralia/node-nstg/blob/master/trl.md">Telegram Recipient Language</see>.
        /// Note that the `census` and `categories` primitives are not implemented.
        /// </summary>
        [JsonProperty]
        public string Recipients { get; }

        /// <summary>
        /// Gets the telegram's ID.
        /// </summary>
        [JsonProperty]
        public long ID { get; }

        /// <summary>
        /// Gets the telegram's key.
        /// </summary>
        [JsonProperty]
        public string Key { get; }

        /// <summary>
        /// Gets the telegram's type.
        /// </summary>
        [JsonProperty]
        public TelegramType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Telegram"/> struct.
        /// </summary>
        /// <param name="clientKey">The user's client key.</param>
        /// <param name="id">The telegram's ID.</param>
        /// <param name="key">The telegram's key.</param>
        /// <param name="type">The telegram's type.</param>
        /// <param name="recipients">The recipients of the telegram in Auralia's <see href="https://github.com/auralia/node-nstg/blob/master/trl.md">Telegram Recipient Language</see>.
        /// Note that the `census` and `categories` primitives are not implemented.</param>
        public Telegram(string clientKey, long id, string key, TelegramType type, string recipients)
        {
            this.ClientKey = clientKey;
            this.ID = id;
            this.Key = key;
            this.Type = type;
            this.Recipients = recipients;
        }

        /// <summary>
        /// Start sending telegrams.
        /// </summary>
        public void Send()
        {
            int delay = this.Type == TelegramType.Recruitment ? 180000 : 30000;
            HashSet<string> nations = GetNationsFromTRL(this.Recipients);

            foreach (string nation in nations)
            {
                DownloadPage($"https://www.nationstates.net/cgi-bin/api.cgi?a=sendTG&client={this.ClientKey}&to={nation}&tgid={this.ID}&key={this.Key}", null, delay);
            }
        }

        /// <summary>
        /// Gets a JSON string representing the cause of death.
        /// </summary>
        /// <returns>A JSON string representing the cause of death.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}
