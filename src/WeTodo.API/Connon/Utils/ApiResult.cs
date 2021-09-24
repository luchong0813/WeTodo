using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WeTodo.API.Connon.Utils
{
    public class ApiResult
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public ApiResult()
        {
            Code = (int)ResultEnum.SUCCESS;
            Message = "操作成功";
        }

        /// <summary>
        /// 操作成功[返回数据]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ApiResult(object data, int code = (int)ResultEnum.SUCCESS, string message = "操作成功")
        {
            Code = code;
            Message = message;
            Data = data;
        }


        /// <summary>
        /// 通用[无需数据]
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        public ApiResult(int code,string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// 通用
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ApiResult(int code, string message, object data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public int Code { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
    }

    public enum ResultEnum
    {
        [Description("操作失败")]
        FAIL = -1,
        [Description("未登录")]
        UN_LOGIN =-999,
        [Description("操作成功")]
        SUCCESS = 1,
    }
}
