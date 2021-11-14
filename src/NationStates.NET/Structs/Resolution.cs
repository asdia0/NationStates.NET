namespace NationStates.NET
{
    using HtmlAgilityPack;
    using Newtonsoft.Json;
    using System;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Represents a World Assembly resolution.
    /// </summary>
    public struct Resolution
    {
        /// <summary>
        /// Gets the resolution's category.
        /// </summary>
        [JsonProperty]
        public dynamic Category { get; }

        /// <summary>
        /// Gets the council the resolution was submitted in.
        /// </summary>
        [JsonProperty]
        public Council Council { get; }

        /// <summary>
        /// Gets the resolution's council ID.
        /// </summary>
        [JsonProperty]
        public long CouncilID { get; }

        /// <summary>
        /// Gets the time at which the resolution was created.
        /// </summary>
        [JsonProperty]
        public DateTime? Created { get; }

        /// <summary>
        /// Gets the body of the resolution.
        /// </summary>
        [JsonProperty]
        public string Description { get; }

        /// <summary>
        /// Gets the resolution's ID.
        /// </summary>
        [JsonProperty]
        public long ID { get; }

        /// <summary>
        /// Gets the time at which the resolution was implemented.
        /// </summary>
        [JsonProperty]
        public DateTime Implemented { get; }

        /// <summary>
        /// Gets the resolution's name.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// Gets the time of promotion.
        /// </summary>
        [JsonProperty]
        public DateTime? Promoted { get; }

        /// <summary>
        /// Gets the name of the nation that proposed the resolution.
        /// </summary>
        [JsonProperty]
        public string Proposer { get; }

        /// <summary>
        /// Gets the council ID of the resolution that repealed this resolution.
        /// </summary>
        [JsonProperty]
        public int? RepealedCouncilID { get; }

        /// <summary>
        /// Gets the ID of the resolution that repealed this resolution.
        /// </summary>
        [JsonProperty]
        public int? RepealedID { get; }

        /// <summary>
        /// Gets the council ID of the resolution this resolution repeals.
        /// </summary>
        [JsonProperty]
        public int? RepealsCouncilID { get; }

        /// <summary>
        /// Gets the ID of the resolution this resolution repeals.
        /// </summary>
        [JsonProperty]
        public int? RepealsID { get; }

        /// <summary>
        /// Gets the resolution's sub-category.
        /// </summary>
        [JsonProperty]
        public dynamic SubCategory { get; }

        /// <summary>
        /// Gets the number of votes for the resolution.
        /// </summary>
        [JsonProperty]
        public long VotesAgainst { get; }

        /// <summary>
        /// Gets the number of votes against the resolution.
        /// </summary>
        [JsonProperty]
        public long VotesFor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resolution"/> struct.
        /// </summary>
        /// <param name="council">The council the resolution was submitted in.</param>
        /// <param name="councilID">The resolution's council ID.</param>
        public Resolution(Council council, long councilID)
        {
            if (council == Council.United_Nations)
            {
                HtmlNode node = ParseHTMLDocument($"https://www.nationstates.net/page=UN_past_resolutions/council=0?start={councilID - 1}").SelectNodes(".//div[@class='WA_thing WA_thing_historical']")[0];

                this.Council = council;
                this.CouncilID = councilID;
                this.ID = councilID;
                this.Category = (UNCategory)ParseEnum(typeof(UNCategory), node.SelectSingleNode(".//div[@class='WA_thing_rbox']/p[1]").InnerText.Replace("Category:", string.Empty));
                this.SubCategory = ParseUNSubCategory(node.SelectSingleNode(".//div[@class='WA_thing_rbox']/p[2]"), this.Category);
                this.Proposer = node.SelectSingleNode(".//span[@class='nnameblock']").InnerText;
                this.Description = node.SelectSingleNode(".//div[@class='WA_thing_body']").InnerText;
                this.Name = node.SelectSingleNode(".//div[@class='WA_thing_header']/h2/a").InnerText;
                this.VotesAgainst = long.Parse(node.SelectSingleNode(".//tbody/tr[2]/td[2]/p/span").InnerText.Replace(",", string.Empty));
                this.VotesFor = long.Parse(node.SelectSingleNode(".//tbody/tr[1]/td[2]/p/span").InnerText.Replace(",", string.Empty));

                if (node.SelectNodes(".//div[@class='WA_thing_repealed']") == null)
                {
                    this.RepealedID = null;
                    this.RepealedCouncilID = null;
                }
                else
                {
                    this.RepealedCouncilID = this.RepealedID = int.Parse(node.SelectSingleNode(".//div[1]/div[2]/p[2]/a/span").InnerText.Replace("UN#", string.Empty));
                }

                if (this.SubCategory is int?)
                {
                    this.RepealsID = this.RepealsCouncilID = this.SubCategory;
                }
                else
                {
                    this.RepealsID = this.RepealsCouncilID = null;
                }

                this.Promoted = null;
                this.Implemented = ParseUnix(node.SelectSingleNode(".//time").Attributes["data-epoch"].Value);
                this.Created = null;
            }
            else
            {
                XmlNode node = ParseXMLDocument($"q=wa={(int)council + 1}&id={councilID}&q=resolution").SelectSingleNode("/WA/RESOLUTION");

                this.Council = council;
                this.CouncilID = councilID;

                XmlNode option = node.SelectSingleNode("OPTION");

                if (council == Council.General_Assembly)
                {
                    this.Category = (GACategory)ParseEnum(typeof(GACategory), node.SelectSingleNode("CATEGORY").InnerText);
                }
                else
                {
                    this.Category = (SCCategory)ParseEnum(typeof(SCCategory), node.SelectSingleNode("CATEGORY").InnerText);
                }

                this.SubCategory = ParseWASubCategory(option, this.Category);
                this.Created = ParseUnix(node.SelectSingleNode("CREATED").InnerText);
                this.Implemented = ParseUnix(node.SelectSingleNode("IMPLEMENTED").InnerText);
                this.Description = node.SelectSingleNode("DESC").InnerText;
                this.Name = node.SelectSingleNode("NAME").InnerText;
                this.Proposer = node.SelectSingleNode("PROPOSED_BY").InnerText;
                this.ID = long.Parse(node.SelectSingleNode("RESID").InnerText);
                this.VotesFor = long.Parse(node.SelectSingleNode("TOTAL_VOTES_FOR").InnerText);
                this.VotesAgainst = long.Parse(node.SelectSingleNode("TOTAL_VOTES_AGAINST").InnerText);
                this.RepealedID = node.SelectNodes("REPEALED").Count == 0 ? null : int.Parse(node.SelectSingleNode("REPEALED").InnerText);
                this.RepealedCouncilID = node.SelectNodes("REPEALED_BY").Count == 0 ? null : int.Parse(node.SelectSingleNode("REPEALED_BY").InnerText);
                this.RepealsID = node.SelectNodes("REPEALS_RESID").Count == 0 ? null : int.Parse(node.SelectSingleNode("REPEALS_RESID").InnerText);
                this.RepealsCouncilID = node.SelectNodes("REPEALS_COUNCILID").Count == 0 ? null : int.Parse(node.SelectSingleNode("REPEALS_COUNCILID").InnerText);
                this.Promoted = node.SelectNodes("PROMOTED").Count == 0 ? null : ParseUnix(node.SelectSingleNode("PROMOTED").InnerText);
            }
        }

        /// <summary>
        /// Gets a JSON string representing the resolution.
        /// </summary>
        /// <returns>A JSON string representing the resolution.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}