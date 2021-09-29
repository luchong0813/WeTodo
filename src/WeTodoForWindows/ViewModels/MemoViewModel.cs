using Prism.Commands;
using Prism.Mvvm;

using System.Collections.ObjectModel;

using WeTodo.Share.Common.Utils;

using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;

using WeTodoForWindows.Service;

namespace WeTodoForWindows.ViewModels
{
    public class MemoViewModel : BindableBase
    {
        private ObservableCollection<MemoDto> memoDtos;
        private readonly IMemoService service;

        public MemoViewModel(IMemoService service)
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            AddCommand = new DelegateCommand(() => IsRightDraweOpen = true);
            this.service = service;
            CreateTest();
        }

        private bool isRightDraweOpen;

        public bool IsRightDraweOpen
        {
            get { return isRightDraweOpen; }
            set { isRightDraweOpen = value; RaisePropertyChanged(); }
        }


        public DelegateCommand AddCommand { get; private set; }

        private async void CreateTest()
        {
            var memos = await service.GetAllAsync(new QueryParameter
            {
                PageNum = 0,
                PageSize = 100
            });
            if (memos.Code==(int)ResultEnum.SUCCESS)
            {
                foreach (var item in memos.Data.Items)
                {
                    MemoDtos.Add(item);
                }

            }
        }

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

    }
}
