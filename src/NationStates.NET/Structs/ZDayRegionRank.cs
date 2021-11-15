namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Defines a region's Z-Day rank.
    /// </summary>
    public struct ZDayRegionRank
    {
        /// <summary>
        /// Gets the region's score.
        /// </summary>
        [JsonProperty]
        public long Score { get; }

        /// <summary>
        /// Gets the region's name.
        /// </summary>
        [JsonProperty]
        public string Region { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZDayRegionRank"/> struct.
        /// </summary>
        /// <param name="region">The region's name.</param>
        /// <param name="score">The region's score.</param>
        public ZDayRegionRank(string region, long score)
        {
            this.Region = region;
            this.Score = score;
        }

        /// <summary>
        /// Gets a JSON string representing the region during Z-Day.
        /// </summary>
        /// <returns>A JSON string representing the region during Z-Day.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}
