namespace NationStates.NET
{
    using System;

    /// <summary>
    /// Defines a World Assembly resolution.
    /// </summary>
    public struct WAResolution
    {
        /// <summary>
        /// Gets the resolution's ID.
        /// </summary>
        public long ID { get; }

        /// <summary>
        /// Gets the resolution's council ID.
        /// </summary>
        public long CouncilID { get; }

        /// <summary>
        /// Gets the resolution's category.
        /// </summary>
        public dynamic Category { get; }

        /// <summary>
        /// Gets the council the resolution was submitted in.
        /// </summary>
        public WACouncil Council { get; }

        /// <summary>
        /// Gets the time at which the resolution was created.
        /// </summary>
        public DateTime Created { get; }

        /// <summary>
        /// Gets the body of the resolution.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the time at which the resolution was implemented.
        /// </summary>
        public DateTime Implemented { get; }

        /// <summary>
        /// Gets the resolution's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the name of the nation that proposed the resolution.
        /// </summary>
        public string Proposer { get; }

        /// <summary>
        /// Gets the ID of the resolution that repealed this resolution.
        /// </summary>
        public int? RepealedID { get; }

        /// <summary>
        /// Gets the council ID of the resolution that repealed this resolution.
        /// </summary>
        public int? RepealedCouncilID { get; }

        /// <summary>
        /// Gets the ID of the resolution this resolution repeals.
        /// </summary>
        public int? RepealsID { get; }

        /// <summary>
        /// Gets the council ID of the resolution this resolution repeals.
        /// </summary>
        public int? RepealsCouncilID { get; }

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
        /// <param name="id">The resolution's ID.</param>
        /// <param name="councilID">The resolution's council ID.</param>
        /// <param name="category">The resolution's category.</param>
        /// <param name="council">The council the resolution was submitted in.</param>
        /// <param name="created">The time at which the resolution was created.</param>
        /// <param name="description">The body of the resolution.</param>
        /// <param name="implemented">The time at which the resolution was implemented.</param>
        /// <param name="name">The resolution's name.</param>
        /// <param name="proposer">The name of the nation that proposed the resolution.</param>
        /// <param name="repealedID">The ID of the resolution that repealed this resolution.</param>
        /// <param name="repealedCouncilID">The council ID of the resolution that repealed this resolution.</param>
        /// <param name="repealsID">The ID of the resolution this resolution repeals.</param>
        /// <param name="repealsCouncilID">The council ID of the resolution this resolution repeals.</param>
        /// <param name="subCategory">The resolution's sub-category.</param>
        /// <param name="votesFor">The number of votes for the resolution.</param>
        /// <param name="votesAgainst">The number of votes against the resolution.</param>
        public WAResolution(long id, long councilID, dynamic category, WACouncil council, DateTime created, string description, DateTime implemented, string name, string proposer, int? repealedID, int? repealedCouncilID, int? repealsID, int? repealsCouncilID, dynamic subCategory, long votesFor, long votesAgainst)
        {
            this.ID = id;
            this.CouncilID = councilID;
            this.Category = category;
            this.Council = council;
            this.Created = created;
            this.Description = description;
            this.Implemented = implemented;
            this.Name = name;
            this.Proposer = proposer;
            this.RepealedID = repealedID;
            this.RepealedCouncilID = repealedCouncilID;
            this.RepealsID = repealsID;
            this.RepealsCouncilID = repealsCouncilID;
            this.SubCategory = subCategory;
            this.VotesAgainst = votesAgainst;
            this.VotesFor = votesFor;
        }
    }
}
