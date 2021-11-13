namespace NationStates.NET
{
    using System;

    public class Program
    {
        public static void Main()
        {
            foreach (ChallengeRank rank in World.ChallengeRank())
            {
                Console.WriteLine(rank);
            }
        }
    }
}