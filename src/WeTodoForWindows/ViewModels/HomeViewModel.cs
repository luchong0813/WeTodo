using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using System;
using System.Collections.ObjectModel;

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
                case "AddTodo": AddTodo(); break;
                case "AddMemo": AddMemo(); break;
            }
        }

        /// <summary>
        /// 添加或编辑待办事项
        /// </summary>
        private async void AddTodo()
        {
            var dialogResult = await dialogService.ShowDialog("AddTodoView", null);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var todo = dialogResult.Parameters.GetValue<TodoDto>("Value");
                //ID大于0表示编辑
                if (todo.Id > 0)
                {

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
        private async void AddMemo()
        {
            var dialogResult = await dialogService.ShowDialog("AddMemoView", null);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");
                //ID大于0表示编辑
                if (memo.Id > 0)
                {

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
