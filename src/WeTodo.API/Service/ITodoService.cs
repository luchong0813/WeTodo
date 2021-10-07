using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.DataContext;
using WeTodo.Share.Common.Utils;

using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;

namespace WeTodo.API.Service
{
    public interface ITodoService:IBaseService<TodoDto>
    {
        Task<ApiResult> GetAllAsync(TodoParmeter parameter);
    }
}
