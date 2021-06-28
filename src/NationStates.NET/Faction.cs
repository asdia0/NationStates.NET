namespace NationStates.NET
{
    using System;

    /// <summary>
    /// Defines a faction during N-Day.
    /// </summary>
    public struct Faction
    {
        /// <summary>
        /// Gets the faction's ID.
        /// </summary>
        public long ID { get; }

        /// <summary>
        /// Gets the faction's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the faction's description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the time when the faction was founded.
        /// </summary>
        public DateTime Founded { get; }

        /// <summary>
        /// Gets the region that founded the faction.
        /// </summary>
        public string Region { get; }

        /// <summary>
        /// Gets the score of the faction (<see cref="Strikes"/> - <see cref="Radiation"/>).
        /// </summary>
        public long Score { get; }

        /// <summary>
        /// Gets the amount of production points the faction has in total.
        /// </summary>
        public long Production { get; }

        /// <summary>
        /// Gets the amount of nukes the faction has in total.
        /// </summary>
        public long Nukes { get; }

        /// <summary>
        /// Gets the amount of shields the faction has in total.
        /// </summary>
        public long Shields { get; }

        /// <summary>
        /// Gets the amount of targets the faction has in total.
        /// </summary>
        public long Targets { get; }

        /// <summary>
        /// Gets the current amount of nukes launched by the faction.
        /// </summary>
        public long Launches { get; }

        /// <summary>
        /// Gets the current amount of nukes incoming towards the faction.
        /// </summary>
        public long Incoming { get; }

        /// <summary>
        /// Gets the number of nukes targeted towards the faction.
        /// </summary>
        public long Targeted { get; }

        /// <summary>
        /// Gets the total number of radiation imposed onto other factions.
        /// </summary>
        public long Strikes { get; }

        /// <summary>
        /// Gets the total number of radiation in the faction.
        /// </summary>
        public long Radiation { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Faction"/> struct.
        /// </summary>
        /// <param name="id">The faction's ID.</param>
        /// <param name="name">The faction's name.</param>
        /// <param name="description">The faction's description.</param>
        /// <param name="founded">The time when the faction was founded.</param>
        /// <param name="region">The region that founded the faction.</param>
        /// <param name="score">The faction's score.</param>
        /// <param name="production">The number of production points the faction has in total.</param>
        /// <param name="nukes">The number of nukes the faction has in total.</param>
        /// <param name="shields">The number of shields the faction has in total.</param>
        /// <param name="targets">The number of targets the faction has in total.</param>
        /// <param name="launches">The current amount of nukes launched by the faction.</param>
        /// <param name="incoming">The current amount of nukes incoming towards the faction.</param>
        /// <param name="targeted">The number of nukes targeted towards the faction.</param>
        /// <param name="strikes">The total number of radiation imposed onto other factions.</param>
        /// <param name="radiation">The total number of radiation in the faction.</param>
        public Faction(long id, string name, string description, DateTime founded, string region, long score, long production, long nukes, long shields, long targets, long launches, long incoming, long targeted, long strikes, long radiation)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.Founded = founded;
            this.Region = region;
            this.Score = score;
            this.Production = production;
            this.Nukes = nukes;
            this.Shields = shields;
            this.Targets = targets;
            this.Launches = launches;
            this.Incoming = incoming;
            this.Targeted = targeted;
            this.Strikes = strikes;
            this.Radiation = radiation;
        }
    }
}
