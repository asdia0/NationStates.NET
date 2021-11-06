namespace NationStates.NET
{
    /// <summary>
    /// Represents an entity's Z-Day statistics.
    /// </summary>
    public struct ZombieStats
    {
        /// <summary>
        /// Gets the number of dead people in millions.
        /// </summary>
        public long Dead { get; }

        /// <summary>
        /// Gets the number of survivors in millions.
        /// </summary>
        public long Survivors { get; }

        /// <summary>
        /// Gets the number of zombies in millions.
        /// </summary>
        public long Zombies { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZombieStats"/> struct.
        /// </summary>
        /// <param name="survivors">Number of survivors in millions.</param>
        /// <param name="zombies">Number of zombies in millions.</param>
        /// <param name="dead">Number of dead people in millions.</param>
        public ZombieStats(long survivors, long zombies, long dead)
        {
            this.Survivors = survivors;
            this.Zombies = zombies;
            this.Dead = dead;
        }
    }
}