namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a nation during Z-Day.
    /// </summary>
    public struct ZombieNation
    {
        /// <summary>
        /// Gets the nation's action.
        /// </summary>
        [JsonProperty]
        public ZombieAction Action { get; }

        /// <summary>
        /// Gets the nation's intended action. If null, see <see cref="Action"/>.
        /// </summary>
        [JsonProperty]
        public ZombieAction? IntendedAction { get; }

        /// <summary>
        /// Gets the nation's stats.
        /// </summary>
        [JsonProperty]
        public ZombieStats Stats { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZombieNation"/> struct.
        /// </summary>
        /// <param name="action">Nation's action.</param>
        /// <param name="intendedAction">Nation's intended action.</param>
        /// <param name="survivors">Number of survivors in millions.</param>
        /// <param name="zombies">Number of zombies in millions.</param>
        /// <param name="dead">Number of dead people in millions.</param>
        public ZombieNation(ZombieAction action, ZombieAction? intendedAction, long survivors, long zombies, long dead)
        {
            this.Action = action;
            this.IntendedAction = intendedAction;
            this.Stats = new(survivors, zombies, dead);
        }

        /// <summary>
        /// Gets a JSON string representing the nation during Z-Day.
        /// </summary>
        /// <returns>A JSON string representing the nation during Z-Day.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}