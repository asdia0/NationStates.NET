namespace NationStates.NET.Example
{
    using NationStates.NET;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            // With NS.NET, you can interact with your nation.
            CreateADispatch();

            // You can also get information about nations, regions, the world and the world assembly.
            GetInformation();
        }

        public static void CreateADispatch()
        {
            // Initialise the nation.
            Nation n = new("dabberwocky");

            // Set the nation's password.
            n.Pin = "totallyLegitPassword";

            // Create a dispatch
            string title = "An interesting title.";
            string text = "Hello world!";
            DispatchCategory category = DispatchCategory.Meta;
            DispatchMeta subCategory = DispatchMeta.Reference;

            n.AddDispatch(title, text, category, subCategory);
        }

        public static void GetInformation()
        {
            // Get a nation's population
            Nation n = new("dabberwocky");
            Console.WriteLine(n.Population);

            // Get a region's delegate
            Region r = new("the united federations");
            Console.WriteLine(r.Delegate);

            // Get the number of nations in the world.
            Console.WriteLine(World.NumNations);

            // Get the number of World Assembly Delegates.
            Console.WriteLine(WorldAssembly.NumDelegates);
        }
    }
}
