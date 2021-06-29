namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            for (int i = 1; i <= 51; i++)
            {
                WAResolution res = WorldAssembly.GetResolution(WACouncil.General_Assembly, i);
                Console.WriteLine(res.Name);
            }
        }
    }
}
