namespace NationStates.NET
{
    using System;

    public class Program
    {
        public static void Main()
        {
            //Card card = new(2503973, 2);
            foreach (Trade trade in World.CardTrades())
            {
                Console.WriteLine(trade);
            }
        }
    }
}