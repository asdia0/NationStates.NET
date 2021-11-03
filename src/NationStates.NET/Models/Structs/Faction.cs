namespace NationStates.NET
{
    using System;
    using System.Xml;

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
        public Faction(long id)
        {
            this.ID = id;

            XmlNode node = Utility.ParseDocument($"q=faction;id={id}")
                .FirstChild;

            this.Name = node.SelectSingleNode("NAME").InnerText;
            this.Description = node.SelectSingleNode("DESC").InnerText;
            this.Founded = Utility.ParseUnix(node.SelectSingleNode("FOUNDED").InnerText);
            this.Region = node.SelectSingleNode("REGION").InnerText;
            this.Score = long.Parse(node.SelectSingleNode("SCORE").InnerText);
            this.Production = long.Parse(node.SelectSingleNode("SCORE").InnerText);
            this.Nukes = long.Parse(node.SelectSingleNode("SCORE").InnerText);
            this.Shields = long.Parse(node.SelectSingleNode("SHIELD").InnerText);
            this.Targets = long.Parse(node.SelectSingleNode("TARGETS").InnerText);
            this.Launches = long.Parse(node.SelectSingleNode("LAUNCHES").InnerText);
            this.Incoming = long.Parse(node.SelectSingleNode("INCOMING").InnerText);
            this.Targeted = long.Parse(node.SelectSingleNode("TARGETED").InnerText);
            this.Strikes = long.Parse(node.SelectSingleNode("STRIKES").InnerText);
            this.Radiation = long.Parse(node.SelectSingleNode("RADIATION").InnerText);
        }
    }
}
