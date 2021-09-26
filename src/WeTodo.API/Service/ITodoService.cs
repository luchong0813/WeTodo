using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.DataContext;

using WeToDo.Share.Dtos;

namespace WeTodo.API.Service
{
    public interface ITodoService:IBaseService<TodoDto>
    {
    }
}
