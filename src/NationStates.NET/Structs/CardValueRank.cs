namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a card value rank.
    /// </summary>
    public struct CardValueRank
    {
        /// <summary>
        /// Gets the card.
        /// </summary>
        [JsonProperty]
        public Card Card { get; }

        /// <summary>
        /// Gets the card's rank.
        /// </summary>
        [JsonProperty]
        public long Rank { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardValueRank"/> struct.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <param name="rank">The card's rank.</param>
        public CardValueRank(Card card, long rank)
        {
            this.Card = card;
            this.Rank = rank;
        }

        /// <summary>
        /// Gets a JSON string representing the rank.
        /// </summary>
        /// <returns>A JSON string representing the rank.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}