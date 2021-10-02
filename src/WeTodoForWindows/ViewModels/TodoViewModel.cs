using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

using System.Collections.ObjectModel;
using System.Threading;

using WeTodo.Share.Common.Utils;

using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;

using WeTodoForWindows.Service;

namespace WeTodoForWindows.ViewModels
{
    public class TodoViewModel : NavigationViewModel
    {
        private ObservableCollection<TodoDto> todoDtos;
        private readonly ITodoService service;

        public TodoViewModel(ITodoService service, IContainerProvider container) : base(container)
        {
            TodoDtos = new ObservableCollection<TodoDto>();
            AddCommand = new DelegateCommand(() => IsRightDraweOpen = true);
            this.service = service;
        }

        private bool isRightDraweOpen;

        public bool IsRightDraweOpen
        {
            get { return isRightDraweOpen; }
            set { isRightDraweOpen = value; RaisePropertyChanged(); }
        }


        public DelegateCommand AddCommand { get; private set; }

        /// <summary>
        /// 获取数据
        /// </summary>
        private async void GetDataAsync()
        {
            UpdateLoading(true);

            var todos = await service.GetAllAsync(new QueryParameter()
            {
                PageNum = 0,
                PageSize = 100
            });
            if (todos.Code == (int)ResultEnum.SUCCESS)
            {
                TodoDtos.Clear();
                foreach (var item in todos.Data.Items)
                {
                    TodoDtos.Add(item);
                }
            }

            UpdateLoading(false);

        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }

        public ObservableCollection<TodoDto> TodoDtos
        {
            get { return todoDtos; }
            set { todoDtos = value; RaisePropertyChanged(); }
        }

    }
}
