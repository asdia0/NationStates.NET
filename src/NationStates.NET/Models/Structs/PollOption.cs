namespace NationStates.NET
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines an option in a poll.
    /// </summary>
    public struct PollOption
    {
        /// <summary>
        /// Gets the option's ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the option's text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the number of votes received.
        /// </summary>
        public int Votes { get; }

        /// <summary>
        /// Gets the name of the nations that voted for the option.
        /// </summary>
        public HashSet<string> Voters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PollOption"/> struct.
        /// </summary>
        /// <param name="id">The options's ID.</param>
        /// <param name="text">The option's text.</param>
        /// <param name="votes">The number of votes received.</param>
        /// <param name="voters">The name of the nations that voted for the option.</param>
        public PollOption(int id, string text, int votes, HashSet<string> voters)
        {
            if (voters.Count != votes)
            {
                throw new NSError("Votes must be the same as the number of voters.");
            }

            this.ID = id;
            this.Text = text;
            this.Votes = votes;
            this.Voters = voters;
        }
    }
}