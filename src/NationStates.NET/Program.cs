namespace NationStates.NET
{
    using System;

    public class Program
    {
        public static void Main()
        {
            Badge badge = new("Amongus", BadgeType.Commended, 10);
            Console.WriteLine(badge);
            //for (int i = 1; i <= 51; i++)
            //{
            //    WAResolution res = WorldAssembly.GetResolution(WACouncil.General_Assembly, i);
            //    Console.WriteLine(res.Name);
            //}
        }
    }
}