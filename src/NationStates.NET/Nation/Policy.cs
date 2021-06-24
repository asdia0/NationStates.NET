namespace NationStates.NET.Nation
{
    /// <summary>
    /// Represents a policy.
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// Name of policy.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category of policy.
        /// </summary>
        public PolicyCategory Category { get; set; }

        /// <summary>
        /// Description of policy.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Policy"/> class.
        /// </summary>
        /// <param name="name">Name of policy.</param>
        /// <param name="category">Category of policy.</param>
        /// <param name="description">Description of policy.</param>
        public Policy(string name, PolicyCategory category, string description)
        {
            this.Name = name;
            this.Category = category;
            this.Description = description;
        }
    }
}
