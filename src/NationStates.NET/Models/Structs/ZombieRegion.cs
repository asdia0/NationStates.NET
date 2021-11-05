namespace NationStates.NET
{
    /// <summary>
    /// Defines a region during Z-Day.
    /// </summary>
    public struct ZombieRegion
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
        /// Initializes a new instance of the <see cref="ZombieRegion"/> struct.
        /// </summary>
        /// <param name="survivors">Number of survivors in millions.</param>
        /// <param name="zombies">Number of zombies in millions.</param>
        /// <param name="dead">Number of dead people in millions.</param>
        public ZombieRegion(long survivors, long zombies, long dead)
        {
            this.Survivors = survivors;
            this.Zombies = zombies;
            this.Dead = dead;
        }
    }
}