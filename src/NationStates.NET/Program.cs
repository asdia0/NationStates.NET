namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            foreach (WorldCensus w in World.GetCensusRanks(74, 1))
            {
                Console.WriteLine((w.Rank, w.Name));
            }
        }
    }
}
