using System;
using System.Windows.Input;

namespace CodingChallenge.ViewModels
{
    /// <summary>
    /// A simple ICommand implementation that delegates to Action/Func.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Creates a new RelayCommand.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Optional predicate to determine if command can execute.</param>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Creates a new RelayCommand with parameterless action.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Optional predicate to determine if command can execute.</param>
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
            : this(_ => execute(), canExecute != null ? _ => canExecute() : null)
        {
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);

        /// <summary>
        /// Raises CanExecuteChanged to re-evaluate command state.
        /// </summary>
        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }
}

