using System.Threading.Tasks;

using WeTodo.API.Connon.Utils;

using WeToDo.Share.Dtos;

namespace WeTodo.API.Service
{
    public interface IAccountService
    {
        Task<ApiResult> LoginAsync(string account, string password);
        Task<ApiResult> RegisterAsync(UserDto user);
    }
}
