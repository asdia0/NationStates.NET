namespace NationStates.NET
{
    /// <summary>
    /// Defines a nation's freedom levels.
    /// </summary>
    public struct Freedom
    {
        /// <summary>
        /// Gets the nation's civil rights level.
        /// </summary>
        public CivilRights CivilRights { get; }

        /// <summary>
        /// Gets the nation's economy level.
        /// </summary>
        public Economy Economy { get; }

        /// <summary>
        /// Gets the nation's political freedoms level.
        /// </summary>
        public PoliticalFreedoms PoliticalFreedoms { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Freedom"/> struct.
        /// </summary>
        /// <param name="civilRights">Civil rights level.</param>
        /// <param name="economy">Economy level.</param>
        /// <param name="politicalFreedoms">Political freedoms level.</param>
        public Freedom(CivilRights civilRights, Economy economy, PoliticalFreedoms politicalFreedoms)
        {
            this.CivilRights = civilRights;
            this.Economy = economy;
            this.PoliticalFreedoms = politicalFreedoms;
        }
    }
}
