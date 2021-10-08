using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

using System;
using System.Collections.ObjectModel;
using System.Linq;

using WeTodo.Share.Common.Utils;

using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;

using WeTodoForWindows.Service;

namespace WeTodoForWindows.ViewModels
{
    public class MemoViewModel : NavigationViewModel
    {
        private ObservableCollection<MemoDto> memoDtos;
        private readonly IMemoService service;

        public MemoViewModel(IMemoService service, IContainerProvider container) : base(container)
        {
            MemoDtos = new ObservableCollection<MemoDto>();
            ExecuteCommand = new DelegateCommand<string>(Execete);
            SelectedCommand = new DelegateCommand<MemoDto>(Selected);
            DeleteCommand = new DelegateCommand<MemoDto>(DeleteTodo);
            this.service = service;
        }

        private bool isRightDraweOpen;

        public bool IsRightDraweOpen
        {
            get { return isRightDraweOpen; }
            set { isRightDraweOpen = value; RaisePropertyChanged(); }
        }

        private MemoDto currentTodo;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public MemoDto CurrentTodo
        {
            get { return currentTodo; }
            set { currentTodo = value; RaisePropertyChanged(); }
        }

        private string serach;
        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Serach
        {
            get { return serach; }
            set { serach = value; RaisePropertyChanged(); }
        }

        private int selectedIndex;
        /// <summary>
        /// 筛选选中项
        /// </summary>

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<MemoDto> SelectedCommand { get; private set; }
        public DelegateCommand<MemoDto> DeleteCommand { get; private set; }

        /// <summary>
        /// 获取数据
        /// </summary>
        private async void GetDataAsync()
        {
            UpdateLoading(true);

            var todos = await service.GetAllAsync(new QueryParameter()
            {
                PageNum = 0,
                PageSize = 100,
                Serach = Serach
            });
            if (todos.Code == (int)ResultEnum.SUCCESS)
            {
                MemoDtos.Clear();
                foreach (var item in todos.Data.Items)
                {
                    MemoDtos.Add(item);
                }
            }

            UpdateLoading(false);

        }

        private async void DeleteTodo(MemoDto obj)
        {
            var deleteResult = await service.DeleteAsync(obj.Id);
            if (deleteResult.Code == (int)ResultEnum.SUCCESS)
            {
                var todo = MemoDtos.FirstOrDefault(t => t.Id == obj.Id);
                if (todo != null)
                {
                    MemoDtos.Remove(todo);
                }
            }
        }

        private void Execete(string obj)
        {
            switch (obj)
            {
                case "Add": Add(); break;
                case "Query": GetDataAsync(); break;
                case "Save": Save(); break;
            }
        }

        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentTodo.Title) ||
                string.IsNullOrWhiteSpace(CurrentTodo.Content))
            {
                return;
            }

            UpdateLoading(true);
            try
            {
                //ID大于0表示编辑，否则即添加
                if (CurrentTodo.Id > 0)
                {
                    var updateResult = await service.UpdateAsync(CurrentTodo);
                    if (updateResult.Code == (int)ResultEnum.SUCCESS)
                    {
                        var todo = MemoDtos.FirstOrDefault(t => t.Id == CurrentTodo.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentTodo.Title;
                            todo.Content = CurrentTodo.Content;
                        }
                    }
                    IsRightDraweOpen = false;
                }
                else
                {
                    var addResult = await service.AddAsync(CurrentTodo);
                    if (addResult.Code == (int)ResultEnum.SUCCESS)
                    {
                        MemoDtos.Add(addResult.Data);
                        IsRightDraweOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        private void Add()
        {
            CurrentTodo = new MemoDto();
            IsRightDraweOpen = true;
        }

        private async void Selected(MemoDto obj)
        {
            UpdateLoading(true);
            try
            {
                var todoResult = await service.GetFirstOfDefaultAsync(obj.Id);
                if (todoResult.Code == (int)ResultEnum.SUCCESS)
                {
                    CurrentTodo = todoResult.Data;
                    IsRightDraweOpen = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataAsync();
        }

        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }

    }
}
