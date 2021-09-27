
using AutoMapper;

using System;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.Connon.Utils;
using WeTodo.API.DataContext;

using WeToDo.Api;
using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;

namespace WeTodo.API.Service
{
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemoService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResult> AddAsync(MemoDto model)
        {
            try
            {
                var todo= mapper.Map<Memo>(model);
                await unitOfWork.GetRepository<Memo>().InsertAsync(todo);
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
                var repository = unitOfWork.GetRepository<Memo>();
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

        public async Task<ApiResult> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var todos = await unitOfWork.GetRepository<Memo>().GetPagedListAsync(predicate:x=>
                    string.IsNullOrEmpty(parameter.Serach)?true:x.Title.Equals(parameter.Serach),
                    pageIndex:parameter.PageNum,
                    pageSize:parameter.PageSize,
                    orderBy:source=>source.OrderByDescending(o=>o.UpdateDate));
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
                .GetRepository<Memo>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return new ApiResult(todo);
        }

        public async Task<ApiResult> UpdateAsync(MemoDto model)
        {
            try
            {
                var dbTodo=mapper.Map<Memo>(model);
                var repository = unitOfWork.GetRepository<Memo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbTodo.Id));
                if (todo == null)
                {
                    return new ApiResult((int)ResultEnum.FAIL, $"查无此数据，无法操作");
                }

                todo.Content = dbTodo.Content;
                todo.Title = dbTodo.Title;
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
