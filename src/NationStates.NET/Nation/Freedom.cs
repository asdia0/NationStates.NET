namespace NationStates.NET.Nation
{
    /// <summary>
    /// Represent's a nation's freedom levels.
    /// </summary>
    public class Freedom
    {
        /// <summary>
        /// Civil rights level.
        /// </summary>
        public CivilRights CivilRights { get; set; }

        /// <summary>
        /// Economy level.
        /// </summary>
        public Economy Economy { get; set; }

        /// <summary>
        /// Political freedoms level.
        /// </summary>
        public PoliticalFreedoms PoliticalFreedoms { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Freedom"/> class.
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
