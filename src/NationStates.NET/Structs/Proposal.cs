﻿namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using static Utility;

    /// <summary>
    /// Defines a World Assembly proposal.
    /// </summary>
    public struct Proposal
    {
        /// <summary>
        /// Gets a list of delegates that approved of the proposal.
        /// </summary>
        [JsonProperty]
        public HashSet<string> Approvals { get; }

        /// <summary>
        /// Gets the proposal's category.
        /// </summary>
        [JsonProperty]
        public dynamic Category { get; }

        /// <summary>
        /// Gets the council the proposal was submitted in.
        /// </summary>
        [JsonProperty]
        public Council Council { get; }

        /// <summary>
        /// Gets the time at which the proposal was created.
        /// </summary>
        [JsonProperty]
        public DateTime Created { get; }

        /// <summary>
        /// Gets the body of the proposal.
        /// </summary>
        [JsonProperty]
        public string Description { get; }

        /// <summary>
        /// Gets the proposal's ID.
        /// </summary>
        [JsonProperty]
        public string ID { get; }

        /// <summary>
        /// Gets the name of the nation that proposed the proposal.
        /// </summary>
        [JsonProperty]
        public string Proposer { get; }

        /// <summary>
        /// Gets the proposal's sub-category.
        /// </summary>
        [JsonProperty]
        public dynamic SubCategory { get; }

        /// <summary>
        /// Gets the proposal's title.
        /// </summary>
        [JsonProperty]
        public string Title { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Proposal"/> struct.
        /// </summary>
        /// <param name="id">The proposal's ID.</param>
        /// <param name="approvals">A list of delegates that approved of the proposal.</param>
        /// <param name="category">The proposal's category.</param>
        /// <param name="council">The council in which the proposal was submitted in.</param>
        /// <param name="created">The time at which the proposal was created.</param>
        /// <param name="description">The body of the proposal.</param>
        /// <param name="name">The proposal's name.</param>
        /// <param name="proposer">The name of the nation that proposed the proposal.</param>
        /// <param name="subCategory">The proposal's sub-category.</param>
        public Proposal(string id, HashSet<string> approvals, dynamic category, Council council, DateTime created, string description, string name, string proposer, dynamic subCategory)
        {
            this.ID = id;
            this.Approvals = approvals;
            this.Category = category;
            this.Council = council;
            this.Created = created;
            this.Description = description;
            this.Title = name;
            this.Proposer = proposer;
            this.SubCategory = subCategory;
        }

        /// <summary>
        /// Gets a JSON string representing the proposal.
        /// </summary>
        /// <returns>A JSON string representing the proposal.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}