namespace NationStates.NET
{
    /// <summary>
    /// Defines a policy.
    /// </summary>
    public struct Policy
    {
        /// <summary>
        /// Gets the policy's category.
        /// </summary>
        public PolicyCategory Category { get; }

        /// <summary>
        /// Gets the policy's description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the policy's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Policy"/> struct.
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