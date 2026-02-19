using CodingChallenge.Models;

namespace CodingChallenge.Checks
{
    /// <summary>
    /// Verifies that the number of beams is within a reasonable range.
    /// </summary>
    public class BeamCountCheck : IPlanCheck
    {
        private const int MinBeams = 1;
        private const int MaxBeams = 20;
        private const int WarningThreshold = 15;

        public string Name => "Beam Count";
        public string Description => "Verifies beam count is within acceptable range";

        public CheckResult Execute(PlanData plan)
        {
            var result = new CheckResult
            {
                CheckName = Name,
                Description = Description,
                Expected = $"{MinBeams}-{MaxBeams} beams",
                Actual = plan.BeamCount.ToString()
            };

            if (plan.BeamCount < MinBeams)
            {
                result.Status = CheckStatus.Fail;
                result.Message = "No beams found in plan";
            }
            else if (plan.BeamCount > MaxBeams)
            {
                result.Status = CheckStatus.Fail;
                result.Message = $"Beam count ({plan.BeamCount}) exceeds maximum of {MaxBeams}";
            }
            else if (plan.BeamCount > WarningThreshold)
            {
                result.Status = CheckStatus.Warning;
                result.Message = $"High beam count ({plan.BeamCount}) - verify if intentional";
            }
            else
            {
                result.Status = CheckStatus.Pass;
                result.Message = $"Beam count ({plan.BeamCount}) is within acceptable range";
            }

            return result;
        }
    }
}

