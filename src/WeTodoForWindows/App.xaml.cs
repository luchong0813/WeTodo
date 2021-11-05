using DryIoc;

using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using WeTodoForWindows.Common;
using WeTodoForWindows.Service;
using WeTodoForWindows.ViewModels;
using WeTodoForWindows.ViewModels.Dialogs;
using WeTodoForWindows.Views;
using WeTodoForWindows.Views.Dialogs;

namespace WeTodoForWindows
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        { 
            return Container.Resolve<MainView>();
        }

        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView", callback => {
                if (callback.Result!=ButtonResult.OK)
                {
                    Application.Current.Shutdown();
                    return;
                }
            });

            var service = App.Current.MainWindow.DataContext as IConfigureService;
            service.Configure();
            base.OnInitialized();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:64576/", serviceKey: "webUrl");

            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<ITodoService, TodoService>();
            containerRegistry.Register<IMemoService, MemoService>();

            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<TodoView, TodoViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingView, SettingViewModel>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView,AboutViewModel>();

            containerRegistry.RegisterForNavigation<AddTodoView, AddTodoViewModel>();
            containerRegistry.RegisterForNavigation<AddMemoView, AddMemoViewModel>();

            //注册自定义对话服务到容器中
            containerRegistry.Register<IDialogHostService, DialogHostService>();

            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();

            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
        }
    }
}
