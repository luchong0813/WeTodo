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
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemoController:ControllerBase
    {
        private readonly IMemoService memoService;

        public MemoController(IMemoService memoService)
        {
            this.memoService = memoService;
        }

        [HttpGet]
        public async Task<ApiResult> GetAll([FromQuery] QueryParameter param) => await memoService.GetAllAsync(param);

        [HttpGet]
        public async Task<ApiResult> GetById(int id) => await memoService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ApiResult> Add([FromBody] MemoDto model) => await memoService.AddAsync(model);

        [HttpPost]
        public async Task<ApiResult> Update([FromBody] MemoDto model) => await memoService.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResult> Delete(int id) => await memoService.DeleteAsync(id);
    }
}
