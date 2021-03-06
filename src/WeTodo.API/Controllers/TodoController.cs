using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.DataContext;
using WeTodo.API.Service;
using WeTodo.Share.Common.Utils;

using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;

namespace WeTodo.API.Controllers
{
    /// <summary>
    /// 待办事项控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TodoController:ControllerBase
    {
        private readonly ITodoService todoService;

        public TodoController(ITodoService todoService)
        {
            this.todoService = todoService; 
        }

        [HttpGet]
        public async Task<ApiResult> GetAll([FromQuery] TodoParmeter param) => await todoService.GetAllAsync(param);

        [HttpGet]
        public async Task<ApiResult> Get(int id) => await todoService.GetByIdAsync(id);

        [HttpGet]
        public async Task<ApiResult> GetSummary() => await todoService.GetSummaryAsync();

        [HttpPost]
        public async Task<ApiResult> Add([FromBody] TodoDto model) => await todoService.AddAsync(model);

        [HttpPost]
        public async Task<ApiResult> Update([FromBody] TodoDto model) => await todoService.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResult> Delete(int id) => await todoService.DeleteAsync(id);
    }
}
