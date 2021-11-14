namespace NationStates.NET
{
    /// <summary>
    /// Defines the types of N-Day specialists.
    /// </summary>
    public enum Specialty
    {
        /// <summary>
        /// Economic specialists. Can accumulate more Production: their cap is 200% higher.
        /// </summary>
        Economic,

        /// <summary>
        /// Intel specialists. Can finalize targets faster. They have no production advantages.
        /// </summary>
        Intel,

        /// <summary>
        /// Military specialists. Builds nukes faster: They receive 50% more nukes when converting production.
        /// </summary>
        Military,

        /// <summary>
        /// Strategic specialists. Builds shields faster: They receive 50% more shields when converting production.
        /// </summary>
        Strategic,
    }
}
