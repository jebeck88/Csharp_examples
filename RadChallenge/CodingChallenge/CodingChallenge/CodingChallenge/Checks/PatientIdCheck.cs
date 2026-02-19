using CodingChallenge.Models;

namespace CodingChallenge.Checks
{
    /// <summary>
    /// Verifies that a valid Patient ID is present in the plan.
    /// </summary>
    public class PatientIdCheck : IPlanCheck
    {
        public string Name => "Patient ID";
        public string Description => "Verifies Patient ID is present";

        public CheckResult Execute(PlanData plan)
        {
            var result = new CheckResult
            {
                CheckName = Name,
                Description = Description,
                Expected = "Non-empty Patient ID",
                Actual = string.IsNullOrWhiteSpace(plan.PatientId) ? "(empty)" : plan.PatientId
            };

            if (!string.IsNullOrWhiteSpace(plan.PatientId))
            {
                result.Status = CheckStatus.Pass;
                result.Message = "Patient ID is present";
            }
            else
            {
                result.Status = CheckStatus.Fail;
                result.Message = "Patient ID is missing or empty";
            }

            return result;
        }
    }
}

