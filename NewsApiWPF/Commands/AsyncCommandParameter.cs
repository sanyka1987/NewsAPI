using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewsApiWPF.Commands
{
    public class AsyncCommandParameter<T> : IAsyncCommandParameter<T>
    {
        private readonly Func<T, Task> _command;
        private readonly Func<T, bool> _canExecute;

        public AsyncCommandParameter(Func<T, Task> command, Func<T, bool> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) ?? true;

        public async Task ExecuteAsync(T parameter) => await _command(parameter);

        async void ICommand.Execute(object parameter) => await ExecuteAsync((T)parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
