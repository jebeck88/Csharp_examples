using CodingChallenge.Models;

namespace CodingChallenge.Checks
{
    /// <summary>
    /// Verifies that the DICOM file is an RT Plan (RTPLAN modality).
    /// </summary>
    public class ModalityCheck : IPlanCheck
    {
        // RT Plan SOP Class UID
        private const string RtPlanSopClassUid = "1.2.840.10008.5.1.4.1.1.481.5";

        public string Name => "Modality Check";
        public string Description => "Verifies file is an RT Plan";

        public CheckResult Execute(PlanData plan)
        {
            var result = new CheckResult
            {
                CheckName = Name,
                Description = Description,
                Expected = "RTPLAN",
                Actual = plan.Modality
            };

            // Check both modality tag and SOP Class UID
            bool isRtPlan = plan.Modality?.ToUpper() == "RTPLAN" || 
                           plan.SopClassUid == RtPlanSopClassUid;

            if (isRtPlan)
            {
                result.Status = CheckStatus.Pass;
                result.Message = "File is a valid RT Plan";
            }
            else if (!string.IsNullOrEmpty(plan.Modality))
            {
                result.Status = CheckStatus.Warning;
                result.Message = $"File modality is {plan.Modality}, not RTPLAN. Some checks may not apply.";
            }
            else
            {
                result.Status = CheckStatus.Fail;
                result.Message = "Could not determine file modality";
            }

            return result;
        }
    }
}

