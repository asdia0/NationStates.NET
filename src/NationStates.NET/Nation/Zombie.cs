namespace NationStates.NET.Nation
{
    public class Zombie
    {
        public ZombieAction Action { get; set; }

        public ZombieAction IntendedAction { get; set; }

        public long Survivors { get; set; }

        public long Zombies { get; set; }

        public long Dead { get; set; }

        public Zombie(ZombieAction action, ZombieAction intendedAction, long survivors, long zombies, long dead)
        {
            this.Action = action;
            this.IntendedAction = intendedAction;
            this.Survivors = survivors;
            this.Zombies = zombies;
            this.Dead = dead;
        }
    }
}
