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
    }
}
