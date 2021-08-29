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
   public class DataCheckingViewModel:BindableBase
    {
        public DataCheckingViewModel()
        {
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskBars();
        }

        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }

        private void CreateTaskBars() {
            TaskBars.Add(new TaskBar { Icon = "ClockFast", Title = "任务事件总量", Content = "81", Gains = 20.3f,Color= "#008D8E", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "ClockCheckOutline", Title = "已完成", Content = "45", Gains = 14.2f, Color = "#10B138", Target = "" });
            TaskBars.Add(new TaskBar { Icon = "ChartLineVariant", Title = "完成比例", Content = "23", Gains = 35.2f, Color= "#0097FF", Target = "" });
        }
    }
}
