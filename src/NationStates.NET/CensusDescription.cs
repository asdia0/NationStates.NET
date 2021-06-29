﻿namespace NationStates.NET
{
    using System.Xml;

    /// <summary>
    /// Defines a census description.
    /// </summary>
    public struct CensusDescription
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the census description for nations.
        /// </summary>
        public string Nation { get; }

        /// <summary>
        /// Gets the census description for regions.
        /// </summary>
        public string Region { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusDescription"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        public CensusDescription(int id)
        {
            this.ID = id;

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?q=censusdesc;scale={this.ID}"));

            XmlNode node = doc.DocumentElement.SelectSingleNode("CENSUSDESC");

            this.Nation = node.SelectSingleNode("NDESC").InnerText;
            this.Region = node.SelectSingleNode("RDESC").InnerText;
        }

        /// <summary>
        /// Converts the <see cref="CensusDescription"/> into a string.
        /// </summary>
        /// <returns>The stringified version of the <see cref="CensusDescription"/>.</returns>
        public override string ToString()
        {
            return $"(this.Nation, this.Region)";
        }
    }
}
