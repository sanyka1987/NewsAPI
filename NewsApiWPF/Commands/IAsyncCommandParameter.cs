using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewsApiWPF.Commands
{
    public interface IAsyncCommandParameter<T> : ICommand
    {
        Task ExecuteAsync(T parameter);
    }
}
