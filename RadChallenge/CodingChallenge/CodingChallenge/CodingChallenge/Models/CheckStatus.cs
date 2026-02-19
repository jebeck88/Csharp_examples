namespace CodingChallenge.Models
{
    /// <summary>
    /// Represents the result status of a plan check.
    /// </summary>
    public enum CheckStatus
    {
        /// <summary>Check has not been run yet.</summary>
        NotRun,
        
        /// <summary>Check passed successfully.</summary>
        Pass,
        
        /// <summary>Check passed but with a warning condition.</summary>
        Warning,
        
        /// <summary>Check failed.</summary>
        Fail
    }
}

