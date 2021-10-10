using MaterialDesignThemes.Wpf;

using Prism.Commands;
using Prism.Services.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeTodoForWindows.Common;

namespace WeTodoForWindows.ViewModels.Dialogs
{
    public class AddTodoViewModel : IDialogHostAware
    {
        public AddTodoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            //检查DialogHost是否已打开（没打开就去调用关闭会抛异常）
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName);
            }

        }

        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters();
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public void OnDialogOpen(IDialogParameters parameters)
        {

        }
    }
}
