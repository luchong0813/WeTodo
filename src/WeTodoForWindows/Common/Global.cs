using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeToDo.Share.Dtos;

namespace WeTodoForWindows.Common
{
    public static class Global
    {
        #region 用户信息
        /// <summary>
        /// 用户账号昵称
        /// </summary>
        public static string Account { get; set; }

        #endregion

        /// <summary>
        /// 获取当前用户对象
        /// </summary>
        /// <returns></returns>
        public static UserDto GetCurrentUser()
        {
            UserDto user = new UserDto();
            user.Account = Account;
            return user;
        }
    }
}
