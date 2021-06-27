namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            Region region = new Region("the united federations");
        
            foreach (Post mes in region.Messages)
            {
                Console.WriteLine(mes.Message);
            }
        }
    }
}
