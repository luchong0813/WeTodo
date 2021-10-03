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

using Prism.Events;

using WeTodoForWindows.Common.Events;
using WeTodoForWindows.Extensions;

namespace WeTodoForWindows.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            eventAggregator.GetEvent<StringMessageEvent>().Subscribe(Execute);

            //注册加载数据窗口
            eventAggregator.Register(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;
                if (DialogHost.IsOpen)
                {
                    DialogHost.DialogContent = new LoadingView();
                }
            });

            //当菜单列表状态发声改变手动去关闭左侧导航
            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
        }

        //使用Prism事件订阅器处理最大化/最小化
        private void Execute(string msg)
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
                    Application.Current.Shutdown();
                    break;
            }
        }

    }
}
