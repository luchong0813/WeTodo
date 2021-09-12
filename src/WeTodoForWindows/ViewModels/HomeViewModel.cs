using Prism.Mvvm;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeTodoForWindows.Models;

namespace WeTodoForWindows.ViewModels
{
    public class HomeViewModel:BindableBase
    {
        public HomeViewModel()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskBars();
            CraetTestDtoData();
        }

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

        private void CreateTaskBars()
        {
            TaskBars.Add(new TaskBar { Icon = "ClockFast", Title = "汇总", Content = "81", Color = "#008D8E", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "ClockCheckOutline", Title = "已完成", Content = "45",  Color = "#10B138", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "ChartLineVariant", Title = "完成比例", Content = "23",  Color = "#0097FF", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "PlaylistStar", Title = "备忘录", Content = "5", Color = "#FFA000", Target = "" });
        }

        private void CraetTestDtoData()
        {
            TodoDtos = new ObservableCollection<TodoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            for (int i = 0; i < 10; i++)
            {
                TodoDtos.Add(new TodoDto { Title = "待办" + i, Content = "Do Somthing..." });
                MemoDtos.Add(new MemoDto { Title = "备忘" + i, Content = "Do Somthing..." });
            }
        }
    }
}
