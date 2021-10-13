using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using System;
using System.Collections.ObjectModel;
using System.Linq;

using WeTodo.Share.Common.Utils;

using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;

using WeTodoForWindows.Common;
using WeTodoForWindows.Models;
using WeTodoForWindows.Service;

namespace WeTodoForWindows.ViewModels
{
    public class HomeViewModel : NavigationViewModel
    {
        private readonly IDialogHostService dialogService;
        private readonly IContainerProvider container;
        private readonly ITodoService todoService;
        private readonly IMemoService memoService;

        public HomeViewModel(IDialogHostService dialogService, IContainerProvider container) : base(container)
        {
            CreateTaskBars();
            ExecuteCommand = new DelegateCommand<string>(Exceute);
            this.dialogService = dialogService;
            this.container = container;
            todoService = container.Resolve<ITodoService>();
            memoService = container.Resolve<IMemoService>();
            TodoDtos = new ObservableCollection<TodoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            EditTodoCommand = new DelegateCommand<TodoDto>(AddTodo);
            EditMemoCommand = new DelegateCommand<MemoDto>(AddMemo);
            TodoCompltedCommand = new DelegateCommand<TodoDto>(TodoComplted);
        }

        #region 属性
        private ObservableCollection<TaskBar> taskBars;
        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TodoDto> todoDtos;
        public ObservableCollection<TodoDto> TodoDtos
        {
            get { return todoDtos; }
            set { todoDtos = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<MemoDto> memoDtos;

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }
        #endregion

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<TodoDto> EditTodoCommand { get; private set; }
        public DelegateCommand<MemoDto> EditMemoCommand { get; private set; }
        public DelegateCommand<TodoDto> TodoCompltedCommand { get; private set; }

        private void CreateTaskBars()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            TaskBars.Add(new TaskBar { Icon = "ClockFast", Title = "汇总", Content = "81", Color = "#008D8E", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "ClockCheckOutline", Title = "已完成", Content = "45", Color = "#10B138", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "ChartLineVariant", Title = "完成比例", Content = "23", Color = "#0097FF", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "PlaylistStar", Title = "备忘录", Content = "5", Color = "#FFA000", Target = "" });
        }

        private void Exceute(string obj)
        {
            switch (obj)
            {
                case "AddTodo": AddTodo(null); ; break;
                case "AddMemo": AddMemo(null); break;
            }
        }

        private async void TodoComplted(TodoDto obj)
        {
            var updateResult = await todoService.UpdateAsync(obj);
            if (updateResult.Code == (int)ResultEnum.SUCCESS)
            {
                var todoModel = TodoDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                if (todoModel != null)
                {
                    TodoDtos.Remove(todoModel);
                }
            }

        }

        /// <summary>
        /// 添加或编辑待办事项
        /// </summary>
        private async void AddTodo(TodoDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
            {
                param.Add("Value", model);
            }

            var dialogResult = await dialogService.ShowDialog("AddTodoView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var todo = dialogResult.Parameters.GetValue<TodoDto>("Value");
                //ID大于0表示编辑
                if (todo.Id > 0)
                {
                    var updateResult = await todoService.UpdateAsync(todo);
                    if (updateResult.Code == (int)ResultEnum.SUCCESS)
                    {
                        var todoModel = TodoDtos.FirstOrDefault(t => t.Id.Equals(model.Id));
                        if (todoModel != null)
                        {
                            todoModel.Title = todo.Title;
                            todoModel.Content = todo.Content;
                        }
                    }
                }
                else
                {
                    todo.Status += 1;
                    var addResult = await todoService.AddAsync(todo);
                    if (addResult.Code == (int)ResultEnum.SUCCESS)
                    {
                        TodoDtos.Add(addResult.Data);
                    }
                }
            }
        }

        /// <summary>
        /// 添加或编辑备忘录
        /// </summary>
        private async void AddMemo(MemoDto model)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
            {
                param.Add("Value", model);
            }

            var dialogResult = await dialogService.ShowDialog("AddMemoView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");
                //ID大于0表示编辑
                if (memo.Id > 0)
                {
                    var updateResult = await memoService.UpdateAsync(memo);
                    if (updateResult.Code == (int)ResultEnum.SUCCESS)
                    {
                        var todoModel = TodoDtos.FirstOrDefault(t => t.Id.Equals(model.Id));
                        if (todoModel != null)
                        {
                            todoModel.Title = memo.Title;
                            todoModel.Content = memo.Content;
                        }
                    }
                }
                else
                {
                    var addResult = await memoService.AddAsync(memo);
                    if (addResult.Code == (int)ResultEnum.SUCCESS)
                    {
                        MemoDtos.Add(addResult.Data);
                    }
                }
            }
        }
    }
}
