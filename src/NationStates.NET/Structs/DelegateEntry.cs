namespace NationStates.NET
{
    using System;
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a Delegate voting on the current proposal at vote.
    /// </summary>
    public struct DelegateEntry
    {
        /// <summary>
        /// Gets the vote the Delegate casted.
        /// </summary>
        [JsonProperty]
        public DelegateAction Action { get; }

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
        /// Initializes a new instance of the <see cref="DelegateEntry"/> struct.
        /// </summary>
        /// <param name="action">The vote the Delegate casted.</param>
        /// <param name="nation">The name of the Delegate.</param>
        /// <param name="timeStamp">The time at which the Delegate voted.</param>
        /// <param name="votes">The number the Delegate has.</param>
        public DelegateEntry(DelegateAction action, string nation, DateTime timeStamp, int votes)
        {
            this.Action = action;
            this.Nation = nation;
            this.TimeStamp = timeStamp;
            this.Votes = votes;
        }

        /// <summary>
        /// Gets a JSON string representing a Delegate voting on the proposal at vote.
        /// </summary>
        /// <returns>A JSON string representing a Delegate voting on the proposal at vote.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}