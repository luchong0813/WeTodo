using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeToDo.Share.Dtos;

namespace WeTodoForWindows.Service
{
    public class TodoService : BaseService<TodoDto>, ITodoService
    {
        public TodoService(HttpRestClient client) : base(client, "Todo")
        {
        }
    }
}
