namespace NationStates.NET
{
    using System;
    using System.Xml;

    /// <summary>
    /// Defines a nation's freedom levels.
    /// </summary>
    public struct Freedom
    {
        /// <summary>
        /// Gets the nation's civil rights level.
        /// </summary>
        public CivilRights CivilRights { get; }

        /// <summary>
        /// Gets the nation's economy level.
        /// </summary>
        public Economy Economy { get; }

        /// <summary>
        /// Gets the nation's political freedoms level.
        /// </summary>
        public PoliticalFreedoms PoliticalFreedoms { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Freedom"/> struct.
        /// </summary>
        /// <param name="nation">The nation's name.</param>
        public Freedom(string nation)
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?nation={nation.Replace(" ", "_")}&q=freedom"));

            XmlNode node = doc.DocumentElement.FirstChild;

            this.CivilRights = (CivilRights)Enum.Parse(typeof(CivilRights), Utility.FormatForEnum(node.SelectSingleNode("CIVILRIGHTS").InnerText));
            this.Economy = (Economy)Enum.Parse(typeof(Economy), Utility.FormatForEnum(node.SelectSingleNode("ECONOMY").InnerText));
            this.PoliticalFreedoms = (PoliticalFreedoms)Enum.Parse(typeof(PoliticalFreedoms), Utility.FormatForEnum(node.SelectSingleNode("POLITICALFREEDOM").InnerText));
        }
    }
}
