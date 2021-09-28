using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeToDo.Share.Common.Utils
{
    public class ApiResponse
    {
        public int Code { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
    }

    public class ApiResponse<T>
    {
        public int Code { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
    }
}
