using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeToDo.Share.Common.Utils
{
    public class ApiResponse
    {
        public int Code { get;  set; }
        public string Message { get;  set; }
        public object Data { get;  set; }
    }

    public class ApiResponse<T>
    {
        public int Code { get;  set; }
        public string Message { get;  set; }
        public T Data { get;  set; }
    }
}
