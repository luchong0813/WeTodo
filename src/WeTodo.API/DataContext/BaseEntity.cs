using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeTodo.API.DataContext
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
