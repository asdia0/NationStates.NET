namespace NationStates.NET
{
    using System;

    /// <summary>
    /// Gets a vote casted by a delegate.
    /// </summary>
    public struct DelegateVote
    {
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
        /// Initializes a new instance of the <see cref="DelegateVote"/> struct.
        /// </summary>
        /// <param name="nation">The name of the delegate.</param>
        /// <param name="timeStamp">The time at which the delegate voted.</param>
        /// <param name="votes">The number of votes the delegate has.</param>
        public DelegateVote(string nation, DateTime timeStamp, int votes)
        {
            this.Nation = nation;
            this.TimeStamp = timeStamp;
            this.Votes = votes;
        }
    }
}
