using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using WeTodoForWindows.Common.Events;
using WeTodoForWindows.Extensions;
using WeTodoForWindows.Models;

namespace WeTodoForWindows.ViewModels
{
    public class MainViewModel
    {
        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal journal;

        public MainViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBars();

            ExecuteCommand = new DelegateCommand<string>((arg) =>
            {
                eventAggregator.GetEvent<StringMessageEvent>().Publish(arg);
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
        }

        public ObservableCollection<MenuBar> MenuBars { get; private set; }

        #region Command
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand MovePrevCommand { get; private set; }
        public DelegateCommand MoveNextCommand { get; private set; }

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
            MenuBars.Add(new MenuBar { Icon = "ArchiveStar", Title = "收集箱", NameSpace = "InboxView" });
            MenuBars.Add(new MenuBar { Icon = "BookCheck", Title = "数据复盘", NameSpace = "DataCheckingView" });
            MenuBars.Add(new MenuBar { Icon = "Cog", Title = "系统设置", NameSpace = "SettingView" });
        }
    }
}
