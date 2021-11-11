namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using static Utility;

    /// <summary>
    /// Represents a card trade.
    /// </summary>
    public struct Trade
    {
        /// <summary>
        /// Gets the name of the buyer.
        /// </summary>
        [JsonProperty]
        public string Buyer { get; }

        /// <summary>
        /// Gets the ID of the card traded.
        /// </summary>
        [JsonProperty]
        public long CardID { get; }

        /// <summary>
        /// Gets the price the card was traded at.
        /// </summary>
        [JsonProperty]
        public double Price { get; }

        /// <summary>
        /// Gets the rarity of the card traded.
        /// </summary>
        [JsonProperty]
        public Rarity Rarity { get; }

        /// <summary>
        /// Gets the season of the card traded.
        /// </summary>
        [JsonProperty]
        public int Season { get; }

        /// <summary>
        /// Gets the name of the seller.
        /// </summary>
        [JsonProperty]
        public string Seller { get; }

        /// <summary>
        /// Gets the time at which the trade occured.
        /// </summary>
        [JsonProperty]
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trade"/> struct.
        /// </summary>
        /// <param name="cardID">The ID of the card traded.</param>
        /// <param name="season">The season of the card traded.</param>
        /// <param name="rarity">The rarity of the card traded.</param>
        /// <param name="buyer">The name of the buyer.</param>
        /// <param name="seller">The name of the seller.</param>
        /// <param name="price">The price the card was traded at.</param>
        /// <param name="timeStamp">The time at which the trade occured.</param>
        public Trade(long cardID, int season, Rarity rarity, string buyer, string seller, double price, DateTime timeStamp)
        {
            this.CardID = cardID;
            this.Season = season;
            this.Rarity = rarity;
            this.Buyer = buyer;
            this.Seller = seller;
            this.Price = price;
            this.TimeStamp = timeStamp;
        }

        /// <summary>
        /// Gets a JSON string representing the trade.
        /// </summary>
        /// <returns>A JSON string representing the trade.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}