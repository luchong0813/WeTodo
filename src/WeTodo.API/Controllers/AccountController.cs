using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using WeTodo.API.Connon.Utils;
using WeTodo.API.Service;

using WeToDo.Share.Dtos;

namespace WeTodo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<ApiResult> Login(string account, string password)
        {
            return await accountService.LoginAsync(account, password);
        }

        [HttpPost]
        public async Task<ApiResult> Register([FromBody] UserDto user)=>await accountService.RegisterAsync(user);
    }
}
