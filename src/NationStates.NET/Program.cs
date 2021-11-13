namespace NationStates.NET
{
    using System;

    public class Program
    {
        public static void Main()
        {
            foreach (CardValueRank card in World.CardValueRank(1, Rarity.Legendary))
            {
                Console.WriteLine(card);
            }
        }
    }
}