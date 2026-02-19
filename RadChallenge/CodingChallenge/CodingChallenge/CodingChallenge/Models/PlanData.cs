using System.Collections.Generic;

namespace CodingChallenge.Models
{
    /// <summary>
    /// Contains extracted data from a DICOM RT Plan file.
    /// </summary>
    public class PlanData
    {
        /// <summary>Path to the loaded DICOM file.</summary>
        public string FilePath { get; set; } = string.Empty;
        
        /// <summary>Patient ID from the DICOM file.</summary>
        public string PatientId { get; set; } = string.Empty;
        
        /// <summary>Patient name from the DICOM file.</summary>
        public string PatientName { get; set; } = string.Empty;
        
        /// <summary>Plan label/ID.</summary>
        public string PlanId { get; set; } = string.Empty;
        
        /// <summary>Plan name.</summary>
        public string PlanName { get; set; } = string.Empty;
        
        /// <summary>SOP Class UID (identifies modality type).</summary>
        public string SopClassUid { get; set; } = string.Empty;
        
        /// <summary>Modality (e.g., RTPLAN, RTDOSE, CT).</summary>
        public string Modality { get; set; } = string.Empty;
        
        /// <summary>Number of beams in the plan.</summary>
        public int BeamCount { get; set; }

        public HashSet<string> BeamTypes { get; set; } = new();
        
        /// <summary>Whether the file was successfully loaded.</summary>
        public bool IsLoaded { get; set; }
        
        /// <summary>Error message if loading failed.</summary>
        public string LoadError { get; set; } = string.Empty;
    }
}

