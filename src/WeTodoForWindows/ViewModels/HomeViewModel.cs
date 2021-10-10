using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

using System;
using System.Collections.ObjectModel;

using WeToDo.Share.Dtos;

using WeTodoForWindows.Models;

namespace WeTodoForWindows.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        public HomeViewModel(IDialogService dialogService)
        {
            CreateTaskBars();
            ExecuteCommand = new DelegateCommand<string>(Exceute);
            this.dialogService = dialogService;
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
        private readonly IDialogService dialogService;

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

        private void AddTodo()
        {
            dialogService.ShowDialog("AddTodoView");
        }
        private void AddMemo()
        {
            dialogService.ShowDialog("AddMemoView");
        }

    }
}
