namespace NationStates.NET
{
    using System;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Defines a World Assembly resolution.
    /// </summary>
    public struct WAResolution
    {
        /// <summary>
        /// Gets the resolution's category.
        /// </summary>
        public dynamic Category { get; }

        /// <summary>
        /// Gets the council the resolution was submitted in.
        /// </summary>
        public WACouncil Council { get; }

        /// <summary>
        /// Gets the resolution's council ID.
        /// </summary>
        public long CouncilID { get; }

        /// <summary>
        /// Gets the time at which the resolution was created.
        /// </summary>
        public DateTime Created { get; }

        /// <summary>
        /// Gets the body of the resolution.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the resolution's ID.
        /// </summary>
        public long ID { get; }

        /// <summary>
        /// Gets the time at which the resolution was implemented.
        /// </summary>
        public DateTime Implemented { get; }

        /// <summary>
        /// Gets the resolution's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the time of promotion.
        /// </summary>
        public DateTime? Promoted { get; }

        /// <summary>
        /// Gets the name of the nation that proposed the resolution.
        /// </summary>
        public string Proposer { get; }

        /// <summary>
        /// Gets the council ID of the resolution that repealed this resolution.
        /// </summary>
        public int? RepealedCouncilID { get; }

        /// <summary>
        /// Gets the ID of the resolution that repealed this resolution.
        /// </summary>
        public int? RepealedID { get; }

        /// <summary>
        /// Gets the council ID of the resolution this resolution repeals.
        /// </summary>
        public int? RepealsCouncilID { get; }

        /// <summary>
        /// Gets the ID of the resolution this resolution repeals.
        /// </summary>
        public int? RepealsID { get; }

        /// <summary>
        /// Gets the resolution's sub-category.
        /// </summary>
        public dynamic SubCategory { get; }

        /// <summary>
        /// Gets the number of votes for the resolution.
        /// </summary>
        public long VotesAgainst { get; }

        /// <summary>
        /// Gets the number of votes against the resolution.
        /// </summary>
        public long VotesFor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WAResolution"/> struct.
        /// </summary>
        /// <param name="council">The council the resolution was submitted in.</param>
        /// <param name="councilID">The resolution's council ID.</param>
        public WAResolution(WACouncil council, long councilID)
        {
            XmlNode node = ParseDocument($"q=wa={(int)council + 1}&id={councilID}&q=resolution")
                .SelectSingleNode("/WA/RESOLUTION");

            this.Council = council;
            this.CouncilID = councilID;

            XmlNode option = node.SelectSingleNode("OPTION");

            if (council == WACouncil.General_Assembly)
            {
                this.Category = (WAGACategory)ParseEnum(typeof(WAGACategory), node.SelectSingleNode("CATEGORY").InnerText);
            }
            else
            {
                this.Category = (WASCCategory)ParseEnum(typeof(WASCCategory), node.SelectSingleNode("CATEGORY").InnerText);
            }

            this.SubCategory = ParseSubCategory(option, this.Council, this.Category);
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
}