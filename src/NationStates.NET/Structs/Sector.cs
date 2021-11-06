namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a sector.
    /// </summary>
    public struct Sector
    {
        /// <summary>
        /// Gets the sector's share in the economy in percentage.
        /// </summary>
        [JsonProperty]
        public double Share { get; }

        /// <summary>
        /// Gets the type of sector.
        /// </summary>
        [JsonProperty]
        public SectorType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sector"/> struct.
        /// </summary>
        /// <param name="type">Type of sector.</param>
        /// <param name="share">Sector's share in the economy in percentage.</param>
        public Sector(SectorType type, double share)
        {
            this.Type = type;
            this.Share = share;
        }

        /// <summary>
        /// Gets a JSON string representing the sector.
        /// </summary>
        /// <returns>A JSON string representing the sector.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}