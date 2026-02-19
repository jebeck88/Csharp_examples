using CodingChallenge.Checks;
using CodingChallenge.Models;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Input;

namespace CodingChallenge.ViewModels
{
    /// <summary>
    /// Main ViewModel for the Plan Checker application.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string _filePath = string.Empty;
        private string _statusMessage = "Select a DICOM file to begin";
        private PlanData? _currentPlan;
        private StructureData? _currentStructure;
        private int _passCount;
        private int _failCount;
        private int _warningCount;
        private bool _hasResults;

        private readonly List<IPlanCheck> _planChecks;

        // I'm cheating because i'm short on time! 
        // Really want a separate type for structure types.  Maybe a common parent type
        // for structure and plan checks
        private readonly List<IPlanCheck> _structureChecks;

        public MainViewModel()
        {
            CheckResults = new ObservableCollection<CheckResultViewModel>();
            BrowseCommand = new RelayCommand(ExecuteBrowse);
            RunChecksCommand = new RelayCommand(ExecuteRunPlanChecks, () => _currentPlan?.IsLoaded == true);

            // Register all plan checks
            _planChecks = new List<IPlanCheck>
            {
                new ModalityCheck(),
                new PatientIdCheck(),
                new PlanIdCheck(),
                new BeamCountCheck(), 
                new BeamTypeCheck(),
            };

            _structureChecks = new()
            {
                new SimpleStructureCheck(),
            };
        }

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public int PassCount
        {
            get => _passCount;
            set => SetProperty(ref _passCount, value);
        }

        public int FailCount
        {
            get => _failCount;
            set => SetProperty(ref _failCount, value);
        }

        public int WarningCount
        {
            get => _warningCount;
            set => SetProperty(ref _warningCount, value);
        }

        public bool HasResults
        {
            get => _hasResults;
            set => SetProperty(ref _hasResults, value);
        }

        public ObservableCollection<CheckResultViewModel> CheckResults { get; }

        public ICommand BrowseCommand { get; }
        public ICommand RunChecksCommand { get; }

        private void ExecuteBrowse()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select a DICOM File",
                Filter = "DICOM Files (*.dcm)|*.dcm|All Files (*.*)|*.*",
                FilterIndex = 1
            };

            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
                LoadDicomFile(dialog.FileName);
            }
        }

        private void LoadDicomFile(string path)
        {
            CheckResults.Clear();
            HasResults = false;
            PassCount = FailCount = WarningCount = 0;

            HashSet<String> knownModalities = new()
            { 
                "RTSTRUCT", 
                "RTPLAN" 
            };

            try
            {
                StatusMessage = "Loading DICOM file...";

                var dcm = DICOMObject.Read(path);

                // Get the modality tag
                var modality = dcm.FindFirst(TagHelper.Modality)?.DData?.ToString() ?? string.Empty;

                // Do we support this kind of file?
                if (!knownModalities.Contains(modality))
                {
                    throw new ArgumentException($"DICOM files of modality \"{modality}\" are not supported");
                }

                if (modality == "RTPLAN")
                {

                    _currentPlan = ExtractPlanData(dcm, path);

                    if (_currentPlan.IsLoaded)
                    {
                        StatusMessage = $"Loaded: {Path.GetFileName(path)} - Ready to run checks";
                        ExecuteRunPlanChecks();
                    }

                    else
                    {
                        StatusMessage = $"Error: {_currentPlan.LoadError}";
                    }
                }

                else if (modality == "RTSTRUCT")
                {
                    _currentStructure = ExtractStructureData(dcm, path);

                    if (_currentStructure.IsLoaded)
                    {
                        StatusMessage = $"Loaded: {Path.GetFileName(path)} - Ready to run checks";
                        ExecuteRunStructureChecks();
                    }

                    else
                    {
                        StatusMessage = $"Error: {_currentStructure.LoadError}";
                    }
                }
            }
            catch (Exception ex)
            {
                _currentPlan = new PlanData { IsLoaded = false, LoadError = ex.Message };
                StatusMessage = $"Error loading file: {ex.Message}";
            }
        }

        private PlanData ExtractPlanData(DICOMObject dcm, string path)
        {
            var plan = new PlanData
            {
                FilePath = path,
                IsLoaded = true
            };

            try
            {
                // Extract basic patient info
                plan.PatientId = dcm.FindFirst(TagHelper.PatientID)?.DData?.ToString() ?? string.Empty;
                plan.PatientName = dcm.FindFirst(TagHelper.PatientName)?.DData?.ToString() ?? string.Empty;

                // Extract plan info
                plan.PlanId = dcm.FindFirst(TagHelper.RTPlanLabel)?.DData?.ToString() ?? string.Empty;
                plan.PlanName = dcm.FindFirst(TagHelper.RTPlanName)?.DData?.ToString() ?? string.Empty;
                plan.SopClassUid = dcm.FindFirst(TagHelper.SOPClassUID)?.DData?.ToString() ?? string.Empty;
                plan.Modality = dcm.FindFirst(TagHelper.Modality)?.DData?.ToString() ?? string.Empty;

                // Convert and store the beam count
                plan.BeamCount = Convert.ToInt32(dcm.FindFirst(TagHelper.NumberOfBeams)?.DData);

                // Extract the beam types
                plan.BeamTypes = ExtractBeamTypes(dcm);
            }
            catch (Exception ex)
            {
                plan.IsLoaded = false;
                plan.LoadError = $"Error extracting plan data: {ex.Message}";
            }

            return plan;
        }

        private HashSet<string> ExtractBeamTypes(DICOMObject dcm)
        {
            HashSet<string> result = new();
            var beamTypes = dcm.FindAll(TagHelper.BeamType);
            foreach(var beam in beamTypes)
            {
                var beamType = beam.DData?.ToString() ?? "";
                if(!string.IsNullOrEmpty(beamType))
                {
                    result.Add(beamType);
                }
            }

            return result;
        }

        private StructureData ExtractStructureData(DICOMObject dcm, string path)
        {
            var structure = new StructureData
            {
                FilePath = path,
                IsLoaded = true
            };

            try
            {
                // Extract basic patient info
                structure.PatientId = dcm.FindFirst(TagHelper.PatientID)?.DData?.ToString() ?? string.Empty;
                structure.PatientName = dcm.FindFirst(TagHelper.PatientName)?.DData?.ToString() ?? string.Empty;

                // Extract structgure info
                structure.StructureSetName = dcm.FindFirst(TagHelper.StructureSetName)?.DData?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                structure.IsLoaded = false;
                structure.LoadError = $"Error extracting structure data: {ex.Message}";
            }

            return structure;
        }

        private void ExecuteRunPlanChecks()
        {
            if (_currentPlan == null || !_currentPlan.IsLoaded)
                return;

            CheckResults.Clear();
            PassCount = FailCount = WarningCount = 0;

            foreach (var check in _planChecks)
            {
                var result = check.Execute(_currentPlan);
                CheckResults.Add(CheckResultViewModel.FromModel(result));

                switch (result.Status)
                {
                    case CheckStatus.Pass:
                        PassCount++;
                        break;
                    case CheckStatus.Fail:
                        FailCount++;
                        break;
                    case CheckStatus.Warning:
                        WarningCount++;
                        break;
                }
            }

            HasResults = true;
            StatusMessage = $"Checks complete: {PassCount} passed, {FailCount} failed, {WarningCount} warnings";
        }


        private void ExecuteRunStructureChecks()
        {
            if (_currentStructure == null || !_currentStructure.IsLoaded)
                return;

            CheckResults.Clear();
            PassCount = FailCount = WarningCount = 0;

            foreach (var check in _structureChecks)
            {
                var result = check.Execute(_currentStructure);
                CheckResults.Add(CheckResultViewModel.FromModel(result));

                switch (result.Status)
                {
                    case CheckStatus.Pass:
                        PassCount++;
                        break;
                    case CheckStatus.Fail:
                        FailCount++;
                        break;
                    case CheckStatus.Warning:
                        WarningCount++;
                        break;
                }
            }

            HasResults = true;
            StatusMessage = $"Checks complete: {PassCount} passed, {FailCount} failed, {WarningCount} warnings";
        }
    }
}

