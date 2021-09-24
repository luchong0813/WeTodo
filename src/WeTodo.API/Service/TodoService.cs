﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.Connon.Utils;
using WeTodo.API.DataContext;
using WeTodo.API.UnitOfWork;

namespace WeTodo.API.Service
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork unitOfWork;

        public TodoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApiResult> AddAsync(ToDo model)
        {
            try
            {
                await unitOfWork.GetRepository<ToDo>().InsertAsync(model);
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
                await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
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
            return new ApiResult(await unitOfWork.GetRepository<ToDo>().GetAllAsync());
        }

        public async Task<ApiResult> GetByIdAsync(int id)
        {
            var todo= await unitOfWork
                .GetRepository<ToDo>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return new ApiResult(todo);
        }

        public async Task<ApiResult> UpdateAsync(ToDo model)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDo>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));
                if (todo == null)
                {
                    return new ApiResult((int)ResultEnum.FAIL, $"查无此数据，无法操作");
                }

                todo.Content = model.Content;
                todo.Title = model.Title;
                todo.Status = model.Status;
                todo.UpdateDate = DateTime.Now;


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
