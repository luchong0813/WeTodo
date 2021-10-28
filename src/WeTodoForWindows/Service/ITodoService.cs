using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeToDo.Share.Common.Utils;
using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;
using WeToDo.Shared.Contact;

namespace WeTodoForWindows.Service
{
    public interface ITodoService : IBaseService<TodoDto>
    {
        Task<ApiResponse<PagedList<TodoDto>>> GetAllFilterAsync(TodoParmeter parameter);

        Task<ApiResponse<SummaryDto>> GetSummaryAsync();
    }
}
