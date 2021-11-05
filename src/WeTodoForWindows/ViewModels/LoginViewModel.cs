using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
            RegisterUserDto = new RegisterUserDto();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.loginService = loginService;
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login": Login(); break;
                case "LoginOut": LoginOut(); break;
                //跳转注册页面
                case "ResgiterPage": SelectedIndex = 1; break;
                //跳转登录页面
                case "Return": SelectedIndex = 0; break;
                case "Register": Register(); break;
            }
        }

        private async void Register()
        {
            //任一字段为空则不允许注册
            if (string.IsNullOrWhiteSpace(RegisterUserDto.Account) ||
               string.IsNullOrWhiteSpace(RegisterUserDto.UserName) ||
               string.IsNullOrWhiteSpace(RegisterUserDto.PassWord) ||
               string.IsNullOrWhiteSpace(RegisterUserDto.NewPassWord))
                return;

            //两次密码不一致
            if (RegisterUserDto.PassWord != RegisterUserDto.NewPassWord) return;

            var result = await loginService.RegisterAsync(new UserDto
            {
                Account = RegisterUserDto.Account,
                UserName = RegisterUserDto.UserName,
                Password = registerUserDto.PassWord
            });

            if (result.Code == (int)ResultEnum.SUCCESS)
            {
                //注册成功，切换到登录页
                SelectedIndex = 0;
            }

            //Todo:注册失败
        }

        private void LoginOut()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password)) return;

            var result = await loginService.LoginAsync(new UserDto
            {
                UserName = UserName,
                Password = Password
            });

            if (result.Code == (int)ResultEnum.SUCCESS)
            {
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
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        private string password;
        private readonly ILoginService loginService;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

        private int selectedIndex;
        /// <summary>
        /// 注册登录页切换索引
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

        private RegisterUserDto registerUserDto;

        public RegisterUserDto RegisterUserDto
        {
            get { return registerUserDto; }
            set { registerUserDto = value; RaisePropertyChanged(); }
        }

        #endregion
    }
}
