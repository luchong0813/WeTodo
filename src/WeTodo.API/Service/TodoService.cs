
using AutoMapper;

using System;
using System.Threading.Tasks;

using WeTodo.API.Connon.Utils;
using WeTodo.API.DataContext;

using WeToDo.Api;
using WeToDo.Share.Dtos;

namespace WeTodo.API.Service
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TodoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResult> AddAsync(TodoDto model)
        {
            try
            {
                var todo= mapper.Map<ToDo>(model);
                await unitOfWork.GetRepository<ToDo>().InsertAsync(todo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResult(model);
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
               var todo= await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                if (todo==null)
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

        public async Task<ApiResult> GetAllAsync()
        {
            try
            {
               var todos= await unitOfWork.GetRepository<ToDo>().GetAllAsync();
                return new ApiResult(todos);
            }
            catch (Exception ex)
            {
                return new ApiResult((int)ResultEnum.FAIL, ex.Message);
            }
            
        }

        public async Task<ApiResult> GetByIdAsync(int id)
        {
            var todo= await unitOfWork
                .GetRepository<ToDo>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return new ApiResult(todo);
        }

        public async Task<ApiResult> UpdateAsync(TodoDto model)
        {
            try
            {
                var dbTodo=mapper.Map<ToDo>(model);
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
