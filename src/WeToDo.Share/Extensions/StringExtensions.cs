using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WeToDo.Share.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 获取MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetMD5(this string str) {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str));

            var hash= MD5.Create().ComputeHash(Encoding.Default.GetBytes(str));
            return Convert.ToBase64String(hash);
        }
    }
}
