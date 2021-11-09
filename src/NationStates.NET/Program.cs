namespace NationStates.NET
{
    using System;

    public class Program
    {
        public static void Main()
        {
            Nation nation = new Nation("Dabberwocky");
            Console.WriteLine(nation);
            //for (int i = 1; i <= 51; i++)
            //{
            //    WAResolution res = WorldAssembly.GetResolution(WACouncil.General_Assembly, i);
            //    Console.WriteLine(res.Name);
            //}
        }
    }
}