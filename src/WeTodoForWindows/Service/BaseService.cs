using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeToDo.Share.Common.Utils;
using WeToDo.Share.Dtos;
using WeToDo.Share.Parameters;
using WeToDo.Shared.Contact;

namespace WeTodoForWindows.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly HttpRestClient client;
        private readonly string serviceName;

        public BaseService(HttpRestClient client, string serviceName)
        {
            this.client = client;
            this.serviceName = serviceName;
        }

        public async Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest
            {
                Method = Method.POST,
                Route = $"api/{serviceName}/Add",
                Parameter = entity
            };
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<ApiResponse<TEntity>> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest
            {
                Method = Method.DELETE,
                Route = $"api/{serviceName}/Delete?id={id}"
            };
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest
            {
                Method = Method.GET,
                Route = $"api/{serviceName}/GetAll?pageIndex={parameter.PageNum}&" +
                $"pageSize={parameter.PageSize}&" +
                $"serach={parameter.Serach}"
            };
            return await client.ExecuteAsync<PagedList<TEntity>>(request);
        }

        public async Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest
            {
                Method = Method.GET,
                Route = $"api/{serviceName}/Get?id={id}"
            };
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest
            {
                Method = Method.POST,
                Route = $"api/{serviceName}/Update",
                Parameter = entity
            };
            return await client.ExecuteAsync<TEntity>(request);
        }
    }
}
