namespace NationStates.NET
{
    /// <summary>
    /// Represents a challenge rank.
    /// </summary>
    public struct ChallengeRank
    {
        /// <summary>
        /// Gets the nation's rank.
        /// </summary>
        public long Rank { get; }

        /// <summary>
        /// Gets the nation's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the nation's level.
        /// </summary>
        public int Level { get; }

        /// <summary>
        /// Gets the nation's score.
        /// </summary>
        public long Score { get; }

        /// <summary>
        /// Gets the number of times the nation has won.
        /// </summary>
        public long Wins { get; }

        /// <summary>
        /// Gets the number of times the nation has lost.
        /// </summary>
        public long Losses { get; }

        /// <summary>
        /// Gets the nation's win-rate.
        /// </summary>
        public double WinRate { get; }

        /// <summary>
        /// Gets the nation's census speciality.
        /// </summary>
        public string Speciality { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChallengeRank"/> struct.
        /// </summary>
        /// <param name="rank">The nation's rank.</param>
        /// <param name="name">The nation's name.</param>
        /// <param name="level">The nation's level.</param>
        /// <param name="score">The nation's score.</param>
        /// <param name="wins">The number of times the nation has won.</param>
        /// <param name="losses">The number of times the nation has lost.</param>
        /// <param name="winRate">The nation's win-rate.</param>
        /// <param name="speciality">The nation's census speciality.</param>
        public ChallengeRank(long rank, string name, int level, long score, long wins, long losses, double winRate, string speciality)
        {
            this.Rank = rank;
            this.Name = name;
            this.Level = level;
            this.Score = score;
            this.Wins = wins;
            this.Losses = losses;
            this.WinRate = winRate;
            this.Speciality = speciality;
        }
    }
}
