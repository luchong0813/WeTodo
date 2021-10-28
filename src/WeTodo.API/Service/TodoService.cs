
using AutoMapper;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.DataContext;
using WeTodo.Share.Common.Utils;

using WeToDo.Api;
using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;

namespace WeTodo.API.Service
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TodoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResult> AddAsync(TodoDto model)
        {
            try
            {
                var todo = mapper.Map<ToDo>(model);
                await unitOfWork.GetRepository<ToDo>().InsertAsync(todo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResult(todo);
                return new ApiResult((int)ResultEnum.FAIL, "操作失败");
            }
            catch (Exception ex)
            {
                return new ApiResult((int)ResultEnum.FAIL, ex.Message);
            }
        }

        public async Task<ApiResult> DeleteAsync(int id)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                if (todo == null)
                {
                    return new ApiResult((int)ResultEnum.FAIL, $"查无此数据，无法操作");
                }
                repository.Delete(todo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResult();
                return new ApiResult((int)ResultEnum.FAIL, "操作失败");
            }
            catch (Exception ex)
            {
                return new ApiResult((int)ResultEnum.FAIL, ex.Message);
            }
        }

        public async Task<ApiResult> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var todos = await unitOfWork.GetRepository<ToDo>().GetPagedListAsync(predicate: x =>
                     string.IsNullOrEmpty(parameter.Serach) ? true : x.Title.ToLower().Contains(parameter.Serach.ToLower()),
                    pageIndex: parameter.PageNum,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(o => o.UpdateDate));
                return new ApiResult(todos);
            }
            catch (Exception ex)
            {
                return new ApiResult((int)ResultEnum.FAIL, ex.Message);
            }

        }

        public async Task<ApiResult> GetAllAsync(TodoParmeter parameter)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var todos = await repository.GetPagedListAsync(predicate: x =>
                     (string.IsNullOrWhiteSpace(parameter.Serach) ? true : x.Title.Contains(parameter.Serach)) &&
                     (parameter.Status == 0 ? true : x.Status.Equals(parameter.Status)),
                    pageIndex: parameter.PageNum,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(o => o.UpdateDate));
                return new ApiResult(todos);
            }
            catch (Exception ex)
            {
                return new ApiResult((int)ResultEnum.FAIL, ex.Message);
            }
        }

        public async Task<ApiResult> GetByIdAsync(int id)
        {
            var todo = await unitOfWork
                .GetRepository<ToDo>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return new ApiResult(todo);
        }

        public async Task<ApiResult> GetSummaryAsync()
        {
            try
            {
                var todos = await unitOfWork.GetRepository<ToDo>()
                .GetAllAsync(orderBy: source => source
                 .OrderByDescending(t => t.UpdateDate));

                var memos = await unitOfWork.GetRepository<Memo>()
                    .GetAllAsync(orderBy: source => source
                     .OrderByDescending(t => t.UpdateDate));

                SummaryDto summary = new SummaryDto();
                summary.TodoList = new ObservableCollection<TodoDto>(mapper.Map<List<TodoDto>>(todos.Where(t => t.Status == 1)));
                summary.MemoList = new ObservableCollection<MemoDto>(mapper.Map<List<MemoDto>>(memos));
                summary.Sum = todos.Count();
                summary.CompletedCount = todos.Where(t => t.Status == 2).Count();
                summary.MemoCount = memos.Count();
                summary.CompletedRatio = (summary.CompletedCount / (double)summary.Sum).ToString("0%");

                return new ApiResult(summary);
            }
            catch (Exception ex)
            {
                return new ApiResult((int)ResultEnum.FAIL, ex.Message);
            }
        }

        public async Task<ApiResult> UpdateAsync(TodoDto model)
        {
            try
            {
                var dbTodo = mapper.Map<ToDo>(model);
                var repository = unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbTodo.Id));
                if (todo == null)
                {
                    return new ApiResult((int)ResultEnum.FAIL, $"查无此数据，无法操作");
                }

                todo.Content = dbTodo.Content;
                todo.Title = dbTodo.Title;
                todo.Status = dbTodo.Status;
                todo.UpdateDate = DateTime.Now;

                repository.Update(todo);

                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResult(model);
                return new ApiResult((int)ResultEnum.FAIL, "操作失败");
            }
            catch (Exception ex)
            {
                return new ApiResult((int)ResultEnum.FAIL, ex.Message);
            }
        }
    }
}
