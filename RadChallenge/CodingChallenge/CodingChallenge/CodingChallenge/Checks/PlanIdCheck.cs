using System.Text.RegularExpressions;
using CodingChallenge.Models;

namespace CodingChallenge.Checks
{
    /// <summary>
    /// Verifies that Plan ID follows expected naming conventions.
    /// </summary>
    public class PlanIdCheck : IPlanCheck
    {
        // Pattern: alphanumeric with optional underscores/hyphens, 1-16 chars (DICOM limit)
        private static readonly Regex ValidPlanIdPattern = new(@"^[A-Za-z0-9_\-]{1,16}$", RegexOptions.Compiled);

        public string Name => "Plan ID Format";
        public string Description => "Verifies Plan ID follows naming conventions";

        public CheckResult Execute(PlanData plan)
        {
            var result = new CheckResult
            {
                CheckName = Name,
                Description = Description,
                Expected = "Alphanumeric, 1-16 characters",
                Actual = string.IsNullOrWhiteSpace(plan.PlanId) ? "(empty)" : plan.PlanId
            };

            if (string.IsNullOrWhiteSpace(plan.PlanId))
            {
                result.Status = CheckStatus.Fail;
                result.Message = "Plan ID is missing";
                return result;
            }

            if (ValidPlanIdPattern.IsMatch(plan.PlanId))
            {
                result.Status = CheckStatus.Pass;
                result.Message = "Plan ID format is valid";
            }
            else if (plan.PlanId.Length > 16)
            {
                result.Status = CheckStatus.Warning;
                result.Message = $"Plan ID exceeds 16 characters ({plan.PlanId.Length} chars)";
            }
            else
            {
                result.Status = CheckStatus.Warning;
                result.Message = "Plan ID contains special characters";
            }

            return result;
        }
    }
}

