using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeTodo.Share.Common.Utils;

using WeToDo.Share.Dtos;

using WeTodoForWindows.Service;

namespace WeTodoForWindows.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public string Title { get; private set; } = "WeTodo";

        public event Action<IDialogResult> RequestClose;

        public LoginViewModel(ILoginService loginService)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.loginService = loginService;
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login": Login(); break;
                case "LoginOut": LoginOut(); break;
            }
        }

        private void LoginOut()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(Account) || string.IsNullOrWhiteSpace(Password)) return;

            var result= await loginService.LoginAsync(new UserDto
            {
                Account = Account,
                Password = Password
            });

            if (result.Code == (int)ResultEnum.SUCCESS) {
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }

            //登录失败
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            LoginOut();
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
        private readonly ILoginService loginService;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }
        #endregion
    }
}
