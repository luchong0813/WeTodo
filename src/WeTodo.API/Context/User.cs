using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeTodo.API.DataContext
{
    /// <summary>
    /// 账户
    /// </summary>
    public class User:BaseEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
