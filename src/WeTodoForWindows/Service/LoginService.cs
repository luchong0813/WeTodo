using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeToDo.Share.Common.Utils;
using WeToDo.Share.Dtos;

namespace WeTodoForWindows.Service
{
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient restClient;
        private readonly string serviceName = "Account";

        public LoginService(HttpRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<ApiResponse> LoginAsync(UserDto userDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.POST;
            request.Route = $"api/{serviceName}/Login";
            request.Parameter = userDto;
            return await restClient.ExecuteAsync(request);
        }

        public async Task<ApiResponse> RegisterAsync(UserDto userDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = Method.POST;
            request.Route = $"api/{serviceName}/Register";
            request.Parameter = userDto;
            return await restClient.ExecuteAsync(request);
        }
    }
}
