namespace NationStates.NET
{
    /// <summary>
    /// Represents a sector
    /// </summary>
    public class Sector
    {
        /// <summary>
        /// Type of sector.
        /// </summary>
        public SectorType Type { get; set; }

        /// <summary>
        /// Sector's share in the economy in percentage.
        /// </summary>
        public double Share { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Sector"/> class.
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
