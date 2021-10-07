using RestSharp;

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
    public class TodoService : BaseService<TodoDto>, ITodoService
    {
        private readonly HttpRestClient client;

        public TodoService(HttpRestClient client) : base(client, "Todo")
        {
            this.client = client;
        }

        public async Task<ApiResponse<PagedList<TodoDto>>> GetAllFilterAsync(TodoParmeter parameter)
        {
            BaseRequest request = new BaseRequest
            {
                Method = Method.GET,
                Route = $"api/Todo/GetAll?pageIndex={parameter.PageNum}" +
                $"&pageSize={parameter.PageSize}&" +
                $"&serach={parameter.Serach}" +
                $"&status={parameter.Status}"
            };
            return await client.ExecuteAsync<PagedList<TodoDto>>(request);
        }
    }
}
