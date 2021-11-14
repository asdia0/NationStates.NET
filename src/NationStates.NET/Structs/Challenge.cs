namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents information about a nation's challenge statistics.
    /// </summary>
    public struct Challenge
    {
        /// <summary>
        /// Gets the nation's level.
        /// </summary>
        [JsonProperty]
        public int Level { get; }

        /// <summary>
        /// Gets the number of times the nation has lost.
        /// </summary>
        [JsonProperty]
        public long Losses { get; }

        /// <summary>
        /// Gets the nation's name.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// Gets the nation's rank.
        /// </summary>
        [JsonProperty]
        public long Rank { get; }

        /// <summary>
        /// Gets the nation's score.
        /// </summary>
        [JsonProperty]
        public long Score { get; }

        /// <summary>
        /// Gets the nation's census speciality.
        /// </summary>
        [JsonProperty]
        public string Speciality { get; }

        /// <summary>
        /// Gets the nation's win-rate.
        /// </summary>
        [JsonProperty]
        public double WinRate { get; }

        /// <summary>
        /// Gets the number of times the nation has won.
        /// </summary>
        [JsonProperty]
        public long Wins { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Challenge"/> struct.
        /// </summary>
        /// <param name="rank">The nation's rank.</param>
        /// <param name="name">The nation's name.</param>
        /// <param name="level">The nation's level.</param>
        /// <param name="score">The nation's score.</param>
        /// <param name="wins">The number of times the nation has won.</param>
        /// <param name="losses">The number of times the nation has lost.</param>
        /// <param name="winRate">The nation's win-rate.</param>
        /// <param name="speciality">The nation's census speciality.</param>
        public Challenge(long rank, string name, int level, long score, long wins, long losses, double winRate, string speciality)
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

        /// <summary>
        /// Gets a JSON string representing the rank.
        /// </summary>
        /// <returns>A JSON string representing the rank.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}