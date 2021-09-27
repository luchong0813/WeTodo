using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.Connon.Utils;

using WeToDo.Share.Parameters;

namespace WeTodo.API.Service
{
    public interface IBaseService<T>
    {
        Task<ApiResult> GetByIdAsync(int id);
        Task<ApiResult> GetAllAsync(QueryParameter parameter);
        Task<ApiResult> AddAsync(T model);
        Task<ApiResult> UpdateAsync(T model);
        Task<ApiResult> DeleteAsync(int id);
    }
}
