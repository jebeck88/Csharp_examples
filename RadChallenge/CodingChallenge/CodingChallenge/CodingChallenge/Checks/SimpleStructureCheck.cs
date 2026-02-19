using CodingChallenge.Models;
using System.Numerics;

namespace CodingChallenge.Checks
{
    /// <summary>
    /// This is a simple structure check
    /// </summary>
    public class SimpleStructureCheck : IPlanCheck
    {
        private const int MinBeams = 1;
        private const int MaxBeams = 20;
        private const int WarningThreshold = 15;

        public string Name => "Structure Check";
        public string Description => "Checks to make sure the structure set name exists";

        public CheckResult Execute(PlanData plan)
        {
            var result = new CheckResult
            {
                CheckName = Name,
                Description = Description,
                Expected = "A structure set name exists",
                Actual = "name"
            };

            if(plan is StructureData)
            {
                var structure = plan as StructureData;
                if(string.IsNullOrEmpty(structure?.StructureSetName))
                {
                    result.Status = CheckStatus.Fail;
                    result.Message = "Structure set name is missing";
                }
                else
                {
                    result.Status = CheckStatus.Pass;
                    result.Actual = structure?.StructureSetName ?? "";
                }
            }

            return result;
        }


    }
}

