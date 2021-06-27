namespace NationStates.NET
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an option in a poll.
    /// </summary>
    public class PollOption
    {
        /// <summary>
        /// Gets or sets the option's ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the option's text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the number of votes received.
        /// </summary>
        public int Votes { get; set; }

        /// <summary>
        /// Gets or sets the name of the nations that voted for the option.
        /// </summary>
        public HashSet<string> Voters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PollOption"/> class.
        /// </summary>
        /// <param name="id">The options's ID.</param>
        /// <param name="text">The option's text.</param>
        /// <param name="votes">The number of votes received.</param>
        /// <param name="voters">The name of the nations that voted for the option.</param>
        public PollOption(int id, string text, int votes, HashSet<string> voters)
        {
            this.ID = id;
            this.Text = text;
            this.Votes = votes;
            this.Voters = voters;
        }
    }
}
