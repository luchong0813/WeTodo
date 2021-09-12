using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeTodoForWindows.Models
{
    /// <summary>
    /// 基类DTO实体
    /// </summary>
    public class BaseDto
    {
        private int id;
        private DateTime creatDate;
        private DateTime updateDate;

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate
        {
            get { return creatDate; }
            set { creatDate = value; }
        }

        /// <summary>
        /// 更细时间
        /// </summary>
        public DateTime UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

    }
}
