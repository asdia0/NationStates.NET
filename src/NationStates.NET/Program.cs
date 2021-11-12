namespace NationStates.NET
{
    using System;

    public class Program
    {
        public static void Main()
        {
            Nation nation = new("Dabberwocky");
            Console.WriteLine(Utility.NationNameFromID(nation.DBID));
        }
    }
}