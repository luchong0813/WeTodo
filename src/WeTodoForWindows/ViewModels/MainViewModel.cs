using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

using WeToDo.Share.Dtos;

using WeTodoForWindows.Common;
using WeTodoForWindows.Common.Events;
using WeTodoForWindows.Extensions;
using WeTodoForWindows.Models;
using WeTodoForWindows.Service;

namespace WeTodoForWindows.ViewModels
{
    public class MainViewModel : BindableBase, IConfigureService
    {
        private readonly IRegionManager _regionManager;
        private readonly IContainerProvider container;
        private IRegionNavigationJournal journal;

        public MainViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IContainerProvider container)
        {
            _regionManager = regionManager;
            this.container = container;
            MenuBars = new ObservableCollection<MenuBar>();

            ExecuteCommand = new DelegateCommand<string>((arg) =>
            {
                eventAggregator.GetEvent<QuestionEvent>().Publish(arg);
            });
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);

            MovePrevCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                {
                    journal.GoBack();
                }
            });

            MoveNextCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                {
                    journal.GoForward();
                }
            });
            LoginOutCommand = new DelegateCommand(() => { App.LoginOut(container); });
        }

        public ObservableCollection<MenuBar> MenuBars { get; private set; }

        #region Command
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand MovePrevCommand { get; private set; }
        public DelegateCommand MoveNextCommand { get; private set; }
        public DelegateCommand LoginOutCommand { get; private set; }

        #endregion

        #region
        private UserDto currentUser;

        public UserDto CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(); }
        }
        #endregion

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
                return;
            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, callback =>
            {
                journal = callback.Context.NavigationService.Journal;
            });
        }

        private void CreateMenuBars()
        {
            MenuBars.Add(new MenuBar { Icon = "Home", Title = "主页", NameSpace = "HomeView" });
            MenuBars.Add(new MenuBar { Icon = "FormatListChecks", Title = "待办事项", NameSpace = "TodoView" });
            MenuBars.Add(new MenuBar { Icon = "Book", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar { Icon = "Cog", Title = "系统设置", NameSpace = "SettingView" });
        }

        /// <summary>
        /// 配置应用初始化参数
        /// </summary>
        public void Configure()
        {
            CurrentUser=Global.GetCurrentUser();

            CreateMenuBars();
            _regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate("HomeView");
        }
    }
}
