namespace NationStates.NET
{
    /// <summary>
    /// Represents a nation during Z-Day.
    /// </summary>
    public struct ZombieNation
    {
        /// <summary>
        /// Gets the nation's action.
        /// </summary>
        public ZombieAction Action { get; }

        /// <summary>
        /// Gets the nation's intended action. If null, see <see cref="Action"/>.
        /// </summary>
        public ZombieAction? IntendedAction { get; }

        /// <summary>
        /// Gets the nation's stats.
        /// </summary>
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
    }
}