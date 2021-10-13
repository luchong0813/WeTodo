using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeToDo.Share.Dtos
{
    public class SummaryDto : BaseDto
    {
        private int sum;
        private int completedCount;
        private int memoCount;
        private string completedRatio;
        private ObservableCollection<TodoDto> todoList;
        private ObservableCollection<MemoDto> memoList;

        public int Sum
        {
            get { return sum; }
            set { sum = value; OnPropertyChanged(); }
        }

        public int CompletedCount
        {
            get { return completedCount; }
            set { completedCount = value; OnPropertyChanged(); }
        }

        public int MemoCount
        {
            get { return memoCount; }
            set { memoCount = value; OnPropertyChanged(); }
        }

        public string CompletedRatio
        {
            get { return completedRatio; }
            set { completedRatio = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TodoDto> TodoList
        {
            get { return todoList; }
            set { todoList = value; }
        }
        public ObservableCollection<MemoDto> MemoList
        {
            get { return memoList; }
            set { memoList = value; }
        }
    }
}
