using Newtonsoft.Json;

using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeTodo.Share.Common.Utils;

using WeToDo.Share.Common.Utils;

namespace WeTodoForWindows.Service
{
    /// <summary>
    /// 通用HTTP请求类
    /// </summary>
    public class HttpRestClient
    {
        private readonly string apiUrl;
        private readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            client = new RestClient();
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter!=null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter),ParameterType.RequestBody);

            client.BaseUrl=new Uri(apiUrl+baseRequest.Route);
            var response= await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);

            client.BaseUrl = new Uri(apiUrl + baseRequest.Route);
            var response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
        }
    }
}
