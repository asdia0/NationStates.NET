namespace NationStates.NET
{
    using System.Xml;

    /// <summary>
    /// Defines a banner's information.
    /// </summary>
    public struct Banner
    {
        /// <summary>
        /// Gets the banner's ID.
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// Gets the banner's name.
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Gets the banner's validity.
        /// </summary>
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
                XmlNode node = Utility.ParseDocument($"banner;banner={id}")
                    .DocumentElement
                    .SelectSingleNode("BANNERS")
                    .FirstChild;

                this.Name = node.SelectSingleNode("NAME").InnerText;
                this.Validity = node.SelectSingleNode("VALIDITY").InnerText;
            }
        }
    }
}
