namespace NationStates.NET
{
    using System;
    using Nation;

    public class Program
    {
        public static void Main()
        {
            Nation.Nation nation = new Nation.Nation("Dabberwocky");

            Console.WriteLine((nation.Zombie.Action, nation.Zombie.IntendedAction, nation.Zombie.Survivors, nation.Zombie.Zombies, nation.Zombie.Dead));
        }
    }
}
