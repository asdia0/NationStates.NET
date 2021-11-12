namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a card owner.
    /// </summary>
    public struct Owner
    {
        /// <summary>
        /// Gets the number of copies of the card the owner owns.
        /// </summary>
        [JsonProperty]
        public int Copies { get; }

        /// <summary>
        /// Gets the card's ID.
        /// </summary>
        [JsonProperty]
        public long CardID { get; }

        /// <summary>
        /// Gets the name of the owner.
        /// </summary>
        [JsonProperty]
        public string Nation { get; }

        /// <summary>
        /// Gets the card's season.
        /// </summary>
        [JsonProperty]
        public int Season { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Owner"/> struct.
        /// </summary>
        /// <param name="id">The card's ID.</param>
        /// <param name="season">The card's season.</param>
        /// <param name="nation">The name of the owner.</param>
        /// <param name="copies">The number of copies of the card the owner owns.</param>
        public Owner(long id, int season, string nation, int copies)
        {
            this.CardID = id;
            this.Season = season;
            this.Nation = nation;
            this.Copies = copies;
        }

        /// <summary>
        /// Gets a JSON string representing the owner.
        /// </summary>
        /// <returns>A JSON string representing the owner.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}