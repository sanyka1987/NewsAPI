using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewsApiWPF.Commands
{
    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        private readonly Func<T, Task> _command;
        public AsyncCommand(Func<T, Task> command)
        {
            _command = command;
        }
        public bool CanExecute(object parameter) => true;
        public async Task ExecuteAsync(T parameter) => await _command(parameter);
        async void ICommand.Execute(object parameter) => await ExecuteAsync((T)parameter);
        public event EventHandler CanExecuteChanged;
    }
}
