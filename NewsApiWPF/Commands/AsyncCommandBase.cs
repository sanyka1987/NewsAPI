using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewsApiWPF.Commands
{
    public abstract class AsyncCommandBase<T> : IAsyncCommand<T>
    {
        public abstract bool CanExecute(object parameter);

        public async void Execute(object parameter)
        {
            await ExecuteAsync((T)parameter);
        }

        public abstract Task ExecuteAsync(T parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
