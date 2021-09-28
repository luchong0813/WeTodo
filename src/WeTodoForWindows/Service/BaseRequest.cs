using RestSharp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeTodoForWindows.Service
{
    public class BaseRequest
    {
        /// <summary>
        /// 请求类型
        /// </summary>
        public Method Method { get; set; }
        /// <summary>
        /// 请求路由
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// 请求主体类型
        /// </summary>
        public string ContentType { get; set; } = "application/json";
        /// <summary>
        /// 请求参数
        /// </summary>
        public object Parameter { get; set; }
    }
}
