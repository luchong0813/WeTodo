using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MaterialDesignThemes.Wpf;

using Prism.Events;
using Prism.Services.Dialogs;

using WeTodoForWindows.Common;
using WeTodoForWindows.Common.Events;
using WeTodoForWindows.Extensions;

namespace WeTodoForWindows.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        private readonly IDialogHostService dialogHost;

        public MainView(IEventAggregator eventAggregator,IDialogHostService dialogHost)
        {
            InitializeComponent();
            eventAggregator.GetEvent<QuestionEvent>().Subscribe(arg => Execute(arg));

            //注册加载数据窗口
            eventAggregator.Register(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;
                if (DialogHost.IsOpen)
                {
                    DialogHost.DialogContent = new LoadingView();
                }
            });

            //注册信息推送
            eventAggregator.RegisterMessage(arg =>
            {
                Snackbar.MessageQueue.Enqueue(arg.Message);
            },"Nomal");

            //当菜单列表状态发声改变手动去关闭左侧导航
            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
            this.dialogHost = dialogHost;
        }

        //使用Prism事件订阅器处理最大化/最小化
        private async void Execute(string msg)
        {
            switch (msg)
            {
                case "Min":
                    WindowState = WindowState.Minimized;
                    break;
                case "Max":
                    WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
                    break;
                case "Exit":
                    var dialogResult = await dialogHost.Question("温馨提示", "确认退出系统？");
                    if (dialogResult.Result != ButtonResult.OK) return;
                    Application.Current.Shutdown();
                    break;
            }
        }

    }
}
