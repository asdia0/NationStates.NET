namespace NationStates.NET
{
    /// <summary>
    /// Defines a cause of death.
    /// </summary>
    public struct Death
    {
        /// <summary>
        /// Gets the cause of death.
        /// </summary>
        public string Cause { get; }

        /// <summary>
        /// Gets the death's national frequency in percentage.
        /// </summary>
        public double Frequency { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Death"/> struct.
        /// </summary>
        /// <param name="cause">Cause of death.</param>
        /// <param name="frequency">Frequency in percentage.</param>
        public Death(string cause, double frequency)
        {
            if (frequency > 100 || frequency < 0)
            {
                throw new NSError("Frequency must be in the interval [0, 100].");
            }

            this.Cause = cause;
            this.Frequency = frequency;
        }
    }
}