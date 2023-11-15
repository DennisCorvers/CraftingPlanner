using System;
using System.Windows.Input;

namespace CraftingPlanner.Presentation
{
    internal class RelayCommand : ICommand
    {
        private Action<object?> m_execute;

        private Predicate<object?> m_canExecute;

        public RelayCommand(Action<object?> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            m_execute = execute ?? throw new ArgumentNullException(nameof(execute));
            m_canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => m_canExecute?.Invoke(parameter) == true;

        public void Execute(object? parameter) => m_execute(parameter);

        bool ICommand.CanExecute(object? parameter) => CanExecute(parameter);

        void ICommand.Execute(object? parameter) => Execute(parameter);

        private static bool DefaultCanExecute(object? parameter) => true;
    }
}
