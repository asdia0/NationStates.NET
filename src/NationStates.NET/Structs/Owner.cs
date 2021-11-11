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
        /// Gets the name of the owner.
        /// </summary>
        [JsonProperty]
        public string Nation { get; }

        /// <summary>
        /// Gets the number of copies of the card the owner owns.
        /// </summary>
        [JsonProperty]
        public int Copies { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Owner"/> struct.
        /// </summary>
        /// <param name="nation">The name of the owner.</param>
        /// <param name="copies">The number of copies of the card the owner owns.</param>
        public Owner(string nation, int copies)
        {
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
