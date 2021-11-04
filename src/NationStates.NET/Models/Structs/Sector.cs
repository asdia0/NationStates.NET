namespace NationStates.NET
{
    /// <summary>
    /// Defines a sector.
    /// </summary>
    public struct Sector
    {
        /// <summary>
        /// Gets the type of sector.
        /// </summary>
        public SectorType Type { get; }

        /// <summary>
        /// Gets the sector's share in the economy in percentage.
        /// </summary>
        public double Share { get; }

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
    }
}