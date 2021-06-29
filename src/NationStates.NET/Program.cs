namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine(World.GetEntityNumber(EntityType.Nation));
        }
    }
}
