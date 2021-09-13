using Prism.Commands;
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
    public class TodoViewModel : BindableBase
    {
        public TodoViewModel()
        {
            TodoDtos = new ObservableCollection<TodoDto>();
            CreateTest();

            AddCommand = new DelegateCommand(() => IsRightDraweOpen = true);
        }

        private bool isRightDraweOpen;

        public bool IsRightDraweOpen
        {
            get { return isRightDraweOpen; }
            set { isRightDraweOpen = value; RaisePropertyChanged(); }
        }


        public DelegateCommand AddCommand { get; private set; }

        private void CreateTest()
        {
            for (int i = 0; i < 20; i++)
            {
                TodoDtos.Add(new TodoDto
                {
                    Title="标题"+i,
                    Content="测试数据.."
                });
            }
        }

        private ObservableCollection<TodoDto> todoDtos;
        public ObservableCollection<TodoDto> TodoDtos
        {
            get { return todoDtos; }
            set { todoDtos = value;RaisePropertyChanged(); }
        }

    }
}
