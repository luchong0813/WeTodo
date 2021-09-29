using Prism.Commands;
using Prism.Mvvm;

using System.Collections.ObjectModel;

using WeTodo.Share.Common.Utils;

using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;

using WeTodoForWindows.Service;

namespace WeTodoForWindows.ViewModels
{
    public class TodoViewModel : BindableBase
    {
        private ObservableCollection<TodoDto> todoDtos;
        private readonly ITodoService service;

        public TodoViewModel(ITodoService service)
        {
            TodoDtos = new ObservableCollection<TodoDto>();
            AddCommand = new DelegateCommand(() => IsRightDraweOpen = true);
            this.service = service;

            CreateTest();
        }

        private bool isRightDraweOpen;

        public bool IsRightDraweOpen
        {
            get { return isRightDraweOpen; }
            set { isRightDraweOpen = value; RaisePropertyChanged(); }
        }


        public DelegateCommand AddCommand { get; private set; }

        private async void CreateTest()
        {
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
        }

        public ObservableCollection<TodoDto> TodoDtos
        {
            get { return todoDtos; }
            set { todoDtos = value; RaisePropertyChanged(); }
        }

    }
}
