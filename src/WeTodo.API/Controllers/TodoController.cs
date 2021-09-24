using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeTodo.API.Connon.Utils;
using WeTodo.API.DataContext;
using WeTodo.API.Service;

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
        public async Task<ApiResult> Get() => await todoService.GetAllAsync();

        [HttpGet]
        public async Task<ApiResult> GetById(int id) => await todoService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ApiResult> Add([FromBody] ToDo model) => await todoService.AddAsync(model);

        [HttpPost]
        public async Task<ApiResult> Update([FromBody] ToDo model) => await todoService.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResult> Delete(int id) => await todoService.DeleteAsync(id);
    }
}
