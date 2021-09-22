using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeTodo.API.DataContext
{
    /// <summary>
    /// 待办实体
    /// </summary>
    public class ToDo:BaseEntity
    {
        /// <summary>
        ///标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
