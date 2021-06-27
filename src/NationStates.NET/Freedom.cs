namespace NationStates.NET
{
    /// <summary>
    /// Represent's a nation's freedom levels.
    /// </summary>
    public class Freedom
    {
        /// <summary>
        /// Gets or sets the nation's civil rights level.
        /// </summary>
        public CivilRights CivilRights { get; set; }

        /// <summary>
        /// Gets or sets the nation's economy level.
        /// </summary>
        public Economy Economy { get; set; }

        /// <summary>
        /// Gets or sets the nation's political freedoms level.
        /// </summary>
        public PoliticalFreedoms PoliticalFreedoms { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Freedom"/> class.
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
