using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeToDo.Share.Common.Utils;
using WeToDo.Share.Dtos;

namespace WeTodoForWindows.Service
{
    public interface ILoginService
    {
        Task<ApiResponse<UserDto>> LoginAsync(UserDto userDto);
        Task<ApiResponse> RegisterAsync(UserDto userDto);
    }
}
