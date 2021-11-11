namespace NationStates.NET
{
    using System;

    public class Program
    {
        public static void Main()
        {
            Nation nation = new("Racoda");
            foreach (Market market in nation.Asks)
            {
                Console.WriteLine(market);
            }
        }
    }
}