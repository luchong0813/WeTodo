using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            ExecuteCommand = new DelegateCommand<string>(Execete);
            SelectedCommand = new DelegateCommand<TodoDto>(Selected);
            this.service = service;
        }

        private bool isRightDraweOpen;

        public bool IsRightDraweOpen
        {
            get { return isRightDraweOpen; }
            set { isRightDraweOpen = value; RaisePropertyChanged(); }
        }

        private TodoDto currentTodo;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public TodoDto CurrentTodo
        {
            get { return currentTodo; }
            set { currentTodo = value; RaisePropertyChanged(); }
        }

        private string serach;
        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Serach
        {
            get { return serach; }
            set { serach = value; RaisePropertyChanged(); }
        }


        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<TodoDto> SelectedCommand { get; private set; }

        /// <summary>
        /// 获取数据
        /// </summary>
        private async void GetDataAsync()
        {
            UpdateLoading(true);

            var todos = await service.GetAllAsync(new QueryParameter()
            {
                PageNum = 0,
                PageSize = 100,
                Serach = Serach
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

        private void Execete(string obj)
        {
            switch (obj)
            {
                case "Add": Add(); break;
                case "Query": GetDataAsync(); break;
                case "Save": Save(); break;
            }
        }

        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentTodo.Title) ||
                string.IsNullOrWhiteSpace(CurrentTodo.Content))
            {
                return;
            }

            UpdateLoading(true);
            try
            {
                //ID大于0表示编辑，否则即添加
                if (CurrentTodo.Id > 0)
                {
                    var updateResult = await service.UpdateAsync(CurrentTodo);
                    if (updateResult.Code == (int)ResultEnum.SUCCESS)
                    {
                        var todo = TodoDtos.FirstOrDefault(t => t.Id == CurrentTodo.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentTodo.Title;
                            todo.Content = CurrentTodo.Content;
                            todo.Status = CurrentTodo.Status;
                        }
                    }
                    IsRightDraweOpen = false;
                }
                else
                {
                    var addResult = await service.AddAsync(CurrentTodo);
                    if (addResult.Code == (int)ResultEnum.SUCCESS)
                    {
                        TodoDtos.Add(addResult.Data);
                        IsRightDraweOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 添加待办
        /// </summary>
        private void Add()
        {
            CurrentTodo=new TodoDto();
            IsRightDraweOpen = true;
        }

        private async void Selected(TodoDto obj)
        {
            UpdateLoading(true);
            try
            {
                var todoResult = await service.GetFirstOfDefaultAsync(obj.Id);
                if (todoResult.Code == (int)ResultEnum.SUCCESS)
                {
                    CurrentTodo = todoResult.Data;
                    IsRightDraweOpen = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                UpdateLoading(false);
            }
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
