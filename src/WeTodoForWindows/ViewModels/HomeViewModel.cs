using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
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

        private SummaryDto summary;

        public SummaryDto Summary
        {
            get { return summary; }
            set { summary = value; RaisePropertyChanged(); }
        }

        #endregion

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<TodoDto> EditTodoCommand { get; private set; }
        public DelegateCommand<MemoDto> EditMemoCommand { get; private set; }
        public DelegateCommand<TodoDto> TodoCompltedCommand { get; private set; }

        private void CreateTaskBars()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            TaskBars.Add(new TaskBar { Icon = "ClockFast", Title = "汇总", Color = "#008D8E", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "ClockCheckOutline", Title = "已完成", Color = "#10B138", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "ChartLineVariant", Title = "完成比例", Color = "#0097FF", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "PlaylistStar", Title = "备忘录", Color = "#FFA000", Target = "" });
        }

        private void Exceute(string obj)
        {
            switch (obj)
            {
                case "AddTodo": AddTodo(null); ; break;
                case "AddMemo": AddMemo(null); break;
            }
        }

        //待办完成
        private async void TodoComplted(TodoDto obj)
        {
            var updateResult = await todoService.UpdateAsync(obj);
            if (updateResult.Code == (int)ResultEnum.SUCCESS)
            {
                var todoModel = Summary.TodoList.FirstOrDefault(t => t.Id.Equals(obj.Id));
                if (todoModel != null)
                {
                    Summary.TodoList.Remove(todoModel);
                }
            }
            GetSummary();
            Refresh();
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
                        var todoModel = Summary.TodoList.FirstOrDefault(t => t.Id.Equals(model.Id));
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
                        Summary.TodoList.Add(addResult.Data);
                    }
                    GetSummary();
                }
            }
            Refresh();
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
                        var memoModel = Summary.MemoList.FirstOrDefault(t => t.Id.Equals(model.Id));
                        if (memoModel != null)
                        {
                            memoModel.Title = memo.Title;
                            memoModel.Content = memo.Content;
                        }
                    }
                }
                else
                {
                    var addResult = await memoService.AddAsync(memo);
                    if (addResult.Code == (int)ResultEnum.SUCCESS)
                    {
                        Summary.MemoList.Add(addResult.Data);
                    }
                }
            }
            Refresh();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetSummary();

            base.OnNavigatedTo(navigationContext);
        }

        /// <summary>
        /// 获取汇总数据
        /// </summary>
        private async void GetSummary() {
            var summaryResult = await todoService.GetSummaryAsync();
            if (summaryResult.Code == (int)ResultEnum.SUCCESS)
            {
                Summary = (SummaryDto)summaryResult.Data;
                Refresh();
            }
        }

        /// <summary>
        /// 刷新汇总数据
        /// </summary>
        private void Refresh() {
            TaskBars[0].Content = Summary.Sum.ToString();
            TaskBars[1].Content = Summary.CompletedCount.ToString();
            TaskBars[2].Content = Summary.CompletedRatio.ToString();
            TaskBars[3].Content = Summary.MemoCount.ToString();
        }
    }
}
