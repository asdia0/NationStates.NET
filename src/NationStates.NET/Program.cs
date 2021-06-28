namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            foreach (Dispatch d in World.GetDispatchList(null, DispatchCategory.Factbook, DispatchFactbook.Geography, null))
            {
                Console.WriteLine(d.ID);
            }
        }
    }
}
