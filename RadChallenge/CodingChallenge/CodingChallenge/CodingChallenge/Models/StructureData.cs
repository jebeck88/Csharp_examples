using System.Collections.Generic;

namespace CodingChallenge.Models
{
    /// <summary>
    /// Contains extracted data from a DICOM RT Structure file.
    /// 
    /// Cheating!!! Make this extend PlanData so I don't have to re-write everything
    /// </summary>
    public class StructureData : PlanData
    {
        /// <summary>The structure set name</summary>
        public string StructureSetName { get; set; } = string.Empty;
    }
}

