using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApiWPF.Service
{
    public class CommandService
    {
        public async Task OpenUrlAsync(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                await Task.Run(() => System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                }));
            }
        }
    }
}
