﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeTodoForWindows.Models
{
    public class MenuBar
    {
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 菜单命名空间
        /// </summary>
        public string NameSpace { get; set; }
    }
}
