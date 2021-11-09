namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Represents a banner's information.
    /// </summary>
    public struct Banner
    {
        /// <summary>
        /// Gets the banner's ID.
        /// </summary>
        [JsonProperty]
        public string ID { get; }

        /// <summary>
        /// Gets the banner's name.
        /// </summary>
        [JsonProperty]
        public string? Name { get; }

        /// <summary>
        /// Gets the banner's validity.
        /// </summary>
        [JsonProperty]
        public string? Validity { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Banner"/> struct.
        /// </summary>
        /// <param name="id">The banner's ID.</param>
        public Banner(string id)
        {
            this.ID = id;

            if (this.ID.StartsWith("uploads/"))
            {
                this.Name = null;
                this.Validity = null;
            }
            else
            {
                XmlNode node = ParseDocument($"q=banner;banner={id}").SelectSingleNode("/WORLD/BANNERS/BANNER");

                this.Name = node.SelectSingleNode("NAME").InnerText;
                this.Validity = node.SelectSingleNode("VALIDITY").InnerText;
            }
        }

        /// <summary>
        /// Gets a JSON string representing the banner.
        /// </summary>
        /// <returns>A JSON string representing the banner.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}