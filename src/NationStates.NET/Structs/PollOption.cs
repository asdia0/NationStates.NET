namespace NationStates.NET
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents an option in a poll.
    /// </summary>
    public struct PollOption
    {
        /// <summary>
        /// Gets the option's ID.
        /// </summary>
        [JsonProperty]
        public int ID { get; }

        /// <summary>
        /// Gets the option's text.
        /// </summary>
        [JsonProperty]
        public string Text { get; }

        /// <summary>
        /// Gets the name of the nations that voted for the option.
        /// </summary>
        [JsonProperty]
        public HashSet<string> Voters { get; }

        /// <summary>
        /// Gets the number of votes received.
        /// </summary>
        [JsonProperty]
        public int Votes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PollOption"/> struct.
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

        /// <summary>
        /// Gets a JSON string representing the poll option.
        /// </summary>
        /// <returns>A JSON string representing the poll option.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}