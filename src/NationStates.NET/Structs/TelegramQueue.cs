﻿namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents the telegram queue.
    /// </summary>
    public struct TelegramQueue
    {
        /// <summary>
        /// Gets the number of telegrams in the queue sent via the API.
        /// </summary>
        [JsonProperty]
        public long API { get; }

        /// <summary>
        /// Gets the number of telegrams in the queue sent manually.
        /// </summary>
        [JsonProperty]
        public long Manual { get; }

        /// <summary>
        /// Gets the number of telegrams in the queue sent en masse.
        /// </summary>
        [JsonProperty]
        public long Mass { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TelegramQueue"/> struct.
        /// </summary>
        /// <param name="manual">The number of telegrams in the queue sent manually.</param>
        /// <param name="mass">The number of telegrams in the queue sent en masse.</param>
        /// <param name="api">The number of telegrams in the queue sent via the API.</param>
        public TelegramQueue(long manual, long mass, long api)
        {
            this.Manual = manual;
            this.Mass = mass;
            this.API = api;
        }

        /// <summary>
        /// Gets a JSON string representing the telegram queue.
        /// </summary>
        /// <returns>A JSON string representing the telegram queue.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}