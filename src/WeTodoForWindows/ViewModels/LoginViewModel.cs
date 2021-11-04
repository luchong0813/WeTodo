using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeTodoForWindows.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public string Title { get; private set; } = "WeTodo";

        public event Action<IDialogResult> RequestClose;

        public LoginViewModel()
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login":Login(); break;
                case "LoginOut": LoginOut(); break;
            }
        }

        private void LoginOut()
        {
        }

        private void Login()
        {
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }


        public DelegateCommand<string> ExecuteCommand { get; private set; }


        #region 属性
        private string account;

        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }
        #endregion
    }
}
