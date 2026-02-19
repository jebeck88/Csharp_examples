namespace CodingChallenge.Models
{
    /// <summary>
    /// Represents the result of a single plan check.
    /// </summary>
    public class CheckResult
    {
        /// <summary>Name of the check.</summary>
        public string CheckName { get; set; } = string.Empty;
        
        /// <summary>Description of what the check verifies.</summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>Expected value or criteria for passing.</summary>
        public string Expected { get; set; } = string.Empty;
        
        /// <summary>Actual value found in the plan.</summary>
        public string Actual { get; set; } = string.Empty;
        
        /// <summary>Pass/Fail/Warning status.</summary>
        public CheckStatus Status { get; set; } = CheckStatus.NotRun;
        
        /// <summary>Additional message or details about the result.</summary>
        public string Message { get; set; } = string.Empty;
    }
}

