namespace NationStates.NET
{
    using System;

    /// <summary>
    /// Defines a delegate voting on the current proposal at vote.
    /// </summary>
    public struct DelegateEntry
    {
        /// <summary>
        /// Gets the vote the delegate casted.
        /// </summary>
        public WAVote Action { get; }

        /// <summary>
        /// Gets the name of the delegate.
        /// </summary>
        public string Nation { get; }

        /// <summary>
        /// Gets the time at which the delegate voted.
        /// </summary>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the number of votes the delegate has.
        /// </summary>
        public int Votes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateEntry"/> struct.
        /// </summary>
        /// <param name="action">The vote the delegate casted.</param>
        /// <param name="nation">The name of the delegate.</param>
        /// <param name="timeStamp">The time at which the delegate voted.</param>
        /// <param name="votes">The number the delegate has.</param>
        public DelegateEntry(WAVote action, string nation, DateTime timeStamp, int votes)
        {
            this.Action = action;
            this.Nation = nation;
            this.TimeStamp = timeStamp;
            this.Votes = votes;
        }
    }
}
