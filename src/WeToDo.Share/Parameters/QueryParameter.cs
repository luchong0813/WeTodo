using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeToDo.Share.Parameters
{
    public class QueryParameter
    {
        private int pageNum;

        public int PageNum
        {
            get { return pageNum; }
            //因为是从0开始，所以减去1
            set { pageNum = value - 1; }
        }


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
