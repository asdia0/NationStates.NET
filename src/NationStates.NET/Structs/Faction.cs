namespace NationStates.NET
{
    using System;
    using System.Xml;
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a faction during N-Day.
    /// </summary>
    public struct Faction
    {
        /// <summary>
        /// Gets the faction's description.
        /// </summary>
        [JsonProperty]
        public string Description { get; }

        /// <summary>
        /// Gets the time when the faction was founded.
        /// </summary>
        [JsonProperty]
        public DateTime Founded { get; }

        /// <summary>
        /// Gets the faction's ID.
        /// </summary>
        [JsonProperty]
        public long ID { get; }

        /// <summary>
        /// Gets the current amount of nukes incoming towards the faction.
        /// </summary>
        [JsonProperty]
        public long Incoming { get; }

        /// <summary>
        /// Gets the current amount of nukes launched by the faction.
        /// </summary>
        [JsonProperty]
        public long Launches { get; }

        /// <summary>
        /// Gets the faction's name.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// Gets the amount of nukes the faction has in total.
        /// </summary>
        [JsonProperty]
        public long Nukes { get; }

        /// <summary>
        /// Gets the amount of production points the faction has in total.
        /// </summary>
        [JsonProperty]
        public long Production { get; }

        /// <summary>
        /// Gets the total number of radiation in the faction.
        /// </summary>
        [JsonProperty]
        public long Radiation { get; }

        /// <summary>
        /// Gets the region that founded the faction.
        /// </summary>
        [JsonProperty]
        public string Region { get; }

        /// <summary>
        /// Gets the score of the faction (<see cref="Strikes"/> - <see cref="Radiation"/>).
        /// </summary>
        [JsonProperty]
        public long Score { get; }

        /// <summary>
        /// Gets the amount of shields the faction has in total.
        /// </summary>
        [JsonProperty]
        public long Shields { get; }

        /// <summary>
        /// Gets the total number of radiation imposed onto other factions.
        /// </summary>
        [JsonProperty]
        public long Strikes { get; }

        /// <summary>
        /// Gets the number of nukes targeted towards the faction.
        /// </summary>
        [JsonProperty]
        public long Targeted { get; }

        /// <summary>
        /// Gets the amount of targets the faction has in total.
        /// </summary>
        [JsonProperty]
        public long Targets { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Faction"/> struct.
        /// </summary>
        /// <param name="id">The faction's ID.</param>
        public Faction(long id)
        {
            this.ID = id;

            XmlNode node = ParseDocument($"q=faction;id={id}").SelectSingleNode("/WORLD/FACTION");

            this.Name = node.SelectSingleNode("NAME").InnerText;
            this.Description = node.SelectSingleNode("DESC").InnerText;
            this.Founded = ParseUnix(node.SelectSingleNode("FOUNDED").InnerText);
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

        /// <summary>
        /// Gets a JSON string representing the faction.
        /// </summary>
        /// <returns>A JSON string representing the faction.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}