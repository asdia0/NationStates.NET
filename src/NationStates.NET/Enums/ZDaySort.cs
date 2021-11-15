namespace NationStates.NET
{
    /// <summary>
    /// Defines the types of Z-Day categories to sort by.
    /// </summary>
    public enum ZDaySort
    {
        /// <summary>
        /// Most survivors + no zombies.
        /// </summary>
        MostSurvivorsNoZombies,

        /// <summary>
        /// Most survivors + no quarantine.
        /// </summary>
        MostSurvivorsNoQuarantine,

        /// <summary>
        /// Most survivors.
        /// </summary>
        MostSurvivors,

        /// <summary>
        /// Most zombies.
        /// </summary>
        MostZombies,

        /// <summary>
        /// Most dead.
        /// </summary>
        MostDead,
    }
}
