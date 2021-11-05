using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using WeTodo.API.Service;
using WeTodo.Share.Common.Utils;

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
        public async Task<ApiResult> Login([FromBody] UserDto user)
        {
            return await accountService.LoginAsync(user.UserName, user.Password);
        }

        [HttpPost]
        public async Task<ApiResult> Register([FromBody] UserDto user)=>await accountService.RegisterAsync(user);
    }
}
