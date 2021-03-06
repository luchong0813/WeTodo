using System.Threading.Tasks;

using WeTodo.Share.Common.Utils;

using WeToDo.Share.Dtos;

namespace WeTodo.API.Service
{
    public interface IAccountService
    {
        Task<ApiResult> LoginAsync(string userName, string password);
        Task<ApiResult> RegisterAsync(UserDto user);
    }
}
