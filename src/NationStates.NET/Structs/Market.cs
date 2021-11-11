namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using static Utility;

    /// <summary>
    /// Represents an ask or a bid for a <see cref="Card"/>.
    /// </summary>
    public struct Market
    {
        /// <summary>
        /// Gets the name of the nation.
        /// </summary>
        [JsonProperty]
        public string Nation { get; }

        /// <summary>
        /// Gets the price asked/bid.
        /// </summary>
        [JsonProperty]
        public double Price { get; }

        /// <summary>
        /// Gets the time at which the ask/bid was placed.
        /// </summary>
        [JsonProperty]
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the type of market (ask/bid).
        /// </summary>
        [JsonProperty]
        public MarketType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Market"/> struct.
        /// </summary>
        /// <param name="nation">The name of the nation.</param>
        /// <param name="price">The price asked/bid.</param>
        /// <param name="timeStamp">The time at which the ask/bid was placed.</param>
        /// <param name="type">The type of market (ask/bid).</param>
        public Market(string nation, double price, DateTime timeStamp, MarketType type)
        {
            this.Nation = nation;
            this.Price = price;
            this.TimeStamp = timeStamp;
            this.Type = type;
        }

        /// <summary>
        /// Gets a JSON string representing the market.
        /// </summary>
        /// <returns>A JSON string representing the market.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}