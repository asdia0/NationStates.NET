namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a policy.
    /// </summary>
    public struct Policy
    {
        /// <summary>
        /// Gets the policy's category.
        /// </summary>
        [JsonProperty]
        public PolicyCategory Category { get; }

        /// <summary>
        /// Gets the policy's description.
        /// </summary>
        [JsonProperty]
        public string Description { get; }

        /// <summary>
        /// Gets the policy's name.
        /// </summary>
        [JsonProperty]
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

        /// <summary>
        /// Gets a JSON string representing the policy.
        /// </summary>
        /// <returns>A JSON string representing the policy.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}