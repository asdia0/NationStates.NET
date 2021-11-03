namespace NationStates.NET
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

            XmlNode node = Utility.ParseDocument($"censusdesc;scale={this.ID}")
                .SelectSingleNode("CENSUSDESC");

            this.Nation = node.SelectSingleNode("NDESC").InnerText;
            this.Region = node.SelectSingleNode("RDESC").InnerText;
        }
    }
}
