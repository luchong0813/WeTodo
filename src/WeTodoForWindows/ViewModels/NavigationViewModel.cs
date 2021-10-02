using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeTodoForWindows.Common.Events;
using WeTodoForWindows.Extensions;

namespace WeTodoForWindows.ViewModels
{
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IContainerProvider container;
        public readonly IEventAggregator aggregator;

        public NavigationViewModel(IContainerProvider container)
        {
            this.container = container;
            aggregator = container.Resolve<IEventAggregator>();
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public void UpdateLoading(bool isOpen)
        {
            aggregator.PubUpdateLoading(new UpdateModel()
            {
                IsOpen = isOpen
            });
        }
    }
}
