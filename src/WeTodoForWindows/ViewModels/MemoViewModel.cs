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
   public class MemoViewModel : BindableBase
    {
        public MemoViewModel()
        {
            MemoDtos = new ObservableCollection<MemoDto>();
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
                MemoDtos.Add(new MemoDto
                {
                    Title = "标题" + i,
                    Content = "测试数据.."
                });
            }
        }

        private ObservableCollection<MemoDto> memoDtos;
        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

    }
}
