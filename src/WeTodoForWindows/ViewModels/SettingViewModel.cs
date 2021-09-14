using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeTodoForWindows.Extensions;
using WeTodoForWindows.Models;

namespace WeTodoForWindows.ViewModels
{
    public class SettingViewModel:BindableBase
    {
        private readonly IRegionManager _regionManager;

        public SettingViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBars();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
        }

        public ObservableCollection<MenuBar> MenuBars { get; private set; }
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        private void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
                return;
            _regionManager.Regions[PrismManager.SettingViewRegionName].RequestNavigate(obj.NameSpace);
        }

        private void CreateMenuBars()
        {
            MenuBars.Add(new MenuBar { Icon = "Palette", Title = "个性化", NameSpace = "SkinView" });
            MenuBars.Add(new MenuBar { Icon = "Cog", Title = "系统设置", NameSpace = "CogView" });
            MenuBars.Add(new MenuBar { Icon = "Information", Title = "关于软件", NameSpace = "AboutView" });
        }
    }
}
