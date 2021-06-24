namespace NationStates.NET
{
    using System;

    public class Program
    {
        public static void Main()
        {
            Nation nation = new Nation("dabberwocky");

            foreach (Dispatch d in nation.DispatchList)
            {
                Console.WriteLine((d.ID, d.Category, d.SubCategory));
            }
        }
    }
}
