using CodingChallenge.Models;

namespace CodingChallenge.Checks
{
    /// <summary>
    /// Interface for all plan checks.
    /// </summary>
    public interface IPlanCheck
    {
        /// <summary>
        /// Unique name identifier for this check.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Human-readable description of what this check verifies.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Executes the check against the provided plan data.
        /// </summary>
        /// <param name="plan">The plan data to check.</param>
        /// <returns>Result of the check including pass/fail status.</returns>
        CheckResult Execute(PlanData plan);
    }
}

