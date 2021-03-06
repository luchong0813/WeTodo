using MaterialDesignThemes.Wpf;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeToDo.Share.Dtos;

using WeTodoForWindows.Common;

namespace WeTodoForWindows.ViewModels.Dialogs
{
    public class AddTodoViewModel : BindableBase, IDialogHostAware
    {
        public AddTodoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private TodoDto model;
        /// <summary>
        /// 添加编辑待办实体
        /// </summary>
        public TodoDto Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }


        private void Cancel()
        {
            //检查DialogHost是否已打开（没打开就去调用关闭会抛异常）
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.Cancel));
            }

        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Model.Title) || string.IsNullOrWhiteSpace(Model.Content)) return;

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters
                {
                    { "Value", Model }
                };
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public void OnDialogOpen(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                Model = parameters.GetValue<TodoDto>("Value");
                Model.Status -= 1;
            }
            else {
                Model = new TodoDto();
            }
        }
    }
}
