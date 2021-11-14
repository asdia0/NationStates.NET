namespace NationStates.NET
{
    using HtmlAgilityPack;
    using Newtonsoft.Json;
    using System;
    using System.Xml;
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
        /// Gets information about the faction.
        /// </summary>
        [JsonProperty]
        public NDay Info { get; }

        /// <summary>
        /// Gets the faction's name.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// Gets the number of nations in the faction.
        /// </summary>
        [JsonProperty]
        public long Nations { get; }

        /// <summary>
        /// Gets the region that founded the faction.
        /// </summary>
        [JsonProperty]
        public string Region { get; }

        /// <summary>
        /// Gets the score of the faction (<see cref="NDay.Strikes"/> - <see cref="NDay.Radiation"/>).
        /// </summary>
        [JsonProperty]
        public long Score { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Faction"/> struct.
        /// </summary>
        /// <param name="id">The faction's ID.</param>
        public Faction(long id)
        {
            this.ID = id;

            HtmlNode htmlNode = ParseHTMLDocument($"https://www.nationstates.net/page=faction/fid={id}");
            XmlNode node = ParseXMLDocument($"q=faction;id={id}").SelectSingleNode("/WORLD/FACTION");

            this.Name = node.SelectSingleNode("NAME").InnerText;
            this.Description = node.SelectSingleNode("DESC").InnerText;
            this.Founded = ParseUnix(node.SelectSingleNode("FOUNDED").InnerText);
            this.Region = node.SelectSingleNode("REGION").InnerText;
            this.Score = long.Parse(node.SelectSingleNode("SCORE").InnerText);
            this.Nations = long.Parse(htmlNode.SelectSingleNode(".//a[@title='Nations']").InnerText.Replace("NATIONS", string.Empty).Replace(",", string.Empty));

            int production = int.Parse(node.SelectSingleNode("SCORE").InnerText);
            long nukes = long.Parse(node.SelectSingleNode("SCORE").InnerText);
            long shields = long.Parse(node.SelectSingleNode("SHIELD").InnerText);
            long targets = long.Parse(node.SelectSingleNode("TARGETS").InnerText);
            long launches = long.Parse(node.SelectSingleNode("LAUNCHES").InnerText);
            long incoming = long.Parse(node.SelectSingleNode("INCOMING").InnerText);
            long targeted = long.Parse(node.SelectSingleNode("TARGETED").InnerText);
            long strikes = long.Parse(node.SelectSingleNode("STRIKES").InnerText);
            int radiation = int.Parse(node.SelectSingleNode("RADIATION").InnerText);
            long intercepts = long.Parse(htmlNode.SelectSingleNode(".//a[@title='Intercepts']").InnerText.Replace("INTERCEPTS", string.Empty).Replace(",", string.Empty));

            this.Info = new(incoming, intercepts, launches, nukes, production, radiation, shields, strikes, targeted, targets);
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