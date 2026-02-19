using CodingChallenge.Models;

namespace CodingChallenge.Checks
{
    /// <summary>
    /// Verifies that the beam type is the same amoung all beams in a plan
    /// </summary>
    public class BeamTypeCheck : IPlanCheck
    {
        private const int MinBeams = 1;
        private const int MaxBeams = 20;
        private const int WarningThreshold = 15;

        public string Name => "Beam Type";
        public string Description => "Verifies that the beam type is the same amoung all beams in a plan";

        public CheckResult Execute(PlanData plan)
        {
            var beamTypes = $"[{string.Join(", ", plan.BeamTypes)}]";
            var result = new CheckResult
            {
                CheckName = Name,
                Description = Description,
                Expected = $"All beams in the plan to be of the same type",
                Actual = $"Beam types found in the file: {beamTypes}"
            };

            if (plan.BeamTypes.Count > 1)
            {
                result.Status = CheckStatus.Fail;
                result.Message = $"Multiple beam types found in the DICOM file: \"{plan.BeamTypes.Count}\"";
            }

            return result;
        }
    }
}

