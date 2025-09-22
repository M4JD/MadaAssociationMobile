using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConnectCareMobile.ViewModels.Base
{
    public interface ICommandAsync<T> : ICommand
    {
        Task ExecuteAsync(T parameter);
        bool CanExecute(T parameter);
    }
    public interface ICommandAsync : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }
    public class CommandAsync<T> : ICommandAsync<T>
    {
        public event EventHandler CanExecuteChanged;
        private bool _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        public CommandAsync(Func<T, Task> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(T parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }
        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }
        void ICommand.Execute(object parameter)
        {
            _ = ExecuteAsync((T)parameter);
        }
        #endregion
    }
    /// <summary>
    /// This command should be used for handling
    /// the multiple clicks the same time.
    /// The contorl will be automatically disabled
    /// until the action ends.
    /// </summary>
    public class CommandAsync : ICommandAsync
    {
        public event EventHandler CanExecuteChanged;
        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        public CommandAsync(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }
        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }
        void ICommand.Execute(object parameter)
        {
            _ = ExecuteAsync();
        }
        #endregion
    }
}
