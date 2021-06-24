namespace NationStates.NET
{
    /// <summary>
    /// Represents a nation during Z-Day.
    /// </summary>
    public class NationZombie
    {
        /// <summary>
        /// Nation's action.
        /// </summary>
        public ZombieAction Action { get; set; }

        /// <summary>
        /// Nation's intended action. If null, see <see cref="Action"/>.
        /// </summary>
        public ZombieAction? IntendedAction { get; set; }

        /// <summary>
        /// Number of survivors in millions.
        /// </summary>
        public long Survivors { get; set; }

        /// <summary>
        /// Number of zombies in millions.
        /// </summary>
        public long Zombies { get; set; }

        /// <summary>
        /// Number of dead people in millions.
        /// </summary>
        public long Dead { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="NationZombie"/> class.
        /// </summary>
        /// <param name="action">Nation's action.</param>
        /// <param name="intendedAction">Nation;s intended action.</param>
        /// <param name="survivors">Number of survivors in millions.</param>
        /// <param name="zombies">Number of zombies in millions.</param>
        /// <param name="dead">Number of dead people in millions.</param>
        public NationZombie(ZombieAction action, ZombieAction? intendedAction, long survivors, long zombies, long dead)
        {
            this.Action = action;
            this.IntendedAction = intendedAction;
            this.Survivors = survivors;
            this.Zombies = zombies;
            this.Dead = dead;
        }
    }
}
