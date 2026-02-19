using CodingChallenge.Models;

namespace CodingChallenge.ViewModels
{
    /// <summary>
    /// ViewModel wrapper for a CheckResult to enable UI binding.
    /// </summary>
    public class CheckResultViewModel : ViewModelBase
    {
        private string _checkName = string.Empty;
        private string _description = string.Empty;
        private string _expected = string.Empty;
        private string _actual = string.Empty;
        private CheckStatus _status = CheckStatus.NotRun;
        private string _message = string.Empty;

        public string CheckName
        {
            get => _checkName;
            set => SetProperty(ref _checkName, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Expected
        {
            get => _expected;
            set => SetProperty(ref _expected, value);
        }

        public string Actual
        {
            get => _actual;
            set => SetProperty(ref _actual, value);
        }

        public CheckStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        /// <summary>
        /// Creates a CheckResultViewModel from a CheckResult model.
        /// </summary>
        public static CheckResultViewModel FromModel(CheckResult result)
        {
            return new CheckResultViewModel
            {
                CheckName = result.CheckName,
                Description = result.Description,
                Expected = result.Expected,
                Actual = result.Actual,
                Status = result.Status,
                Message = result.Message
            };
        }
    }
}

