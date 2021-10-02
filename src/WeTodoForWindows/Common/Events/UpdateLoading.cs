using Prism.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeTodoForWindows.Common.Events
{
    public class UpdateModel
    {
        /// <summary>
        /// 是否打开加载页
        /// </summary>
        public bool IsOpen { get; set; }
    }
    public class UpdateLoading : PubSubEvent<UpdateModel>
    {
    }
}
