using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeToDo.Share.Parameters
{
    public class QueryParameter
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNum { get; set; }

        /// <summary>
        /// 页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public string Serach { get; set; }
    }
}
