namespace NationStates.NET
{
    /// <summary>
    /// Represents a policy.
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// Gets or sets the policy's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the policy's category.
        /// </summary>
        public PolicyCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the policy's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Policy"/> class.
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
