using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeTodo.API.DataContext
{
    /// <summary>
    /// 备忘录
    /// </summary>
    public class Memo : BaseEntity
    {
        /// <summary>
        ///标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
