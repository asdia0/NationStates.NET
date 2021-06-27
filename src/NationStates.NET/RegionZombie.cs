namespace NationStates.NET
{
    /// <summary>
    /// Represents a region during Z-Day.
    /// </summary>
    public class RegionZombie
    {
        /// <summary>
        /// Gets or sets the number of survivors in millions.
        /// </summary>
        public long Survivors { get; set; }

        /// <summary>
        /// Gets or sets the number of zombies in millions.
        /// </summary>
        public long Zombies { get; set; }

        /// <summary>
        /// Gets or sets the number of dead people in millions.
        /// </summary>
        public long Dead { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionZombie"/> class.
        /// </summary>
        /// <param name="survivors">Number of survivors in millions.</param>
        /// <param name="zombies">Number of zombies in millions.</param>
        /// <param name="dead">Number of dead people in millions.</param>
        public RegionZombie(long survivors, long zombies, long dead)
        {
            this.Survivors = survivors;
            this.Zombies = zombies;
            this.Dead = dead;
        }
    }
}
