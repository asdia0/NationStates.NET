namespace NationStates.NET
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an option in a poll.
    /// </summary>
    public class PollOption
    {
        /// <summary>
        /// The option's ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The option's text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The number of votes received.
        /// </summary>
        public int Votes { get; set; }

        /// <summary>
        /// The name of the nations that voted for the option.
        /// </summary>
        public HashSet<string> Voters { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="PollOption"/> class.
        /// </summary>
        /// <param name="id">The options's ID.</param>
        /// <param name="text">The option's text.</param>
        /// <param name="votes">The number of votes received.</param>
        /// <param name="voters">The name of the nations that voted for the option.</param>
        public PollOption(int id, string text, string votes, HashSet<string> voters)
        {
            this.ID = id;
            this.Text = text;
            this.Votes = votes;
            this.Voters = voters;
        }
    }
}
