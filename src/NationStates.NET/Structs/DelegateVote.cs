namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using static Utility;

    /// <summary>
    /// Gets a vote casted by a Delegate.
    /// </summary>
    public struct DelegateVote
    {
        /// <summary>
        /// Gets the name of the Delegate.
        /// </summary>
        [JsonProperty]
        public string Nation { get; }

        /// <summary>
        /// Gets the time at which the Delegate voted.
        /// </summary>
        [JsonProperty]
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the number of votes the Delegate has.
        /// </summary>
        [JsonProperty]
        public int Votes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateVote"/> struct.
        /// </summary>
        /// <param name="nation">The name of the Delegate.</param>
        /// <param name="timeStamp">The time at which the Delegate voted.</param>
        /// <param name="votes">The number of votes the Delegate has.</param>
        public DelegateVote(string nation, DateTime timeStamp, int votes)
        {
            this.Nation = nation;
            this.TimeStamp = timeStamp;
            this.Votes = votes;
        }

        /// <summary>
        /// Gets a JSON string representing the vote casted by the Delegate.
        /// </summary>
        /// <returns>A JSON string representing the vote casted by the Delegate.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}