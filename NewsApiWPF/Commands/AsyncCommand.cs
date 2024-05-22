using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewsApiWPF.Commands
{
    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<Task> _command;
        private readonly Func<bool> _canExecute;

        public AsyncCommand(Func<Task> command, Func<bool> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public async Task ExecuteAsync() => await _command();

        async void ICommand.Execute(object parameter) => await ExecuteAsync();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
