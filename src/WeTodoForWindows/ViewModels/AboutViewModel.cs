using Prism.Commands;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WeTodoForWindows.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            OpenBrowserCmd = new DelegateCommand<Uri>((url)=> {
                Process.Start(new ProcessStartInfo(url.ToString()));
            });
        }

        public DelegateCommand<Uri> OpenBrowserCmd { get; private set; }
    }
}
