namespace NationStates.NET.Nation
{
    public class Policy
    {
        public string Name { get; set; }

        public PolicyCategory Category { get; set; }

        public string Description { get; set; }

        public Policy(string name, PolicyCategory category, string description)
        {
            this.Name = name;
            this.Category = category;
            this.Description = description;
        }
    }
}
