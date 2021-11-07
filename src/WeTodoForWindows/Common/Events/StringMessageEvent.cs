using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeTodoForWindows.Common.Events
{
    /// <summary>
    /// 消息事件
    /// </summary>
    public class StringMessageEvent : PubSubEvent<MessageModel>
    {
    }

    /// <summary>
    /// 消息实体
    /// </summary>
    public class MessageModel {
        public string Filter { get; set; }
        public string Message { get; set; }
    }

}
