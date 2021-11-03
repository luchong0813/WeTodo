using Prism.Events;
using Prism.Services.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeTodoForWindows.Common;
using WeTodoForWindows.Common.Events;

namespace WeTodoForWindows.Extensions
{
    public static class DialogExtensions
    {
        /// <summary>
        /// 消息询问对话框
        /// </summary>
        /// <param name="dialogHost">会话主机</param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="dialogHostName">会话主机名称</param>
        /// <returns></returns>
        public static async Task<IDialogResult> Question(this IDialogHostService dialogHost, string title, string content, string dialogHostName = "RootDialog")
        {
            DialogParameters param = new DialogParameters
            {
                { "Title", title },
                { "Content", content },
                { "DialogHostName", dialogHostName }
            };

            var dialogResult = await dialogHost.ShowDialog("MsgView", param, dialogHostName);
            return dialogResult;
        }


        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void PubUpdateLoading(this IEventAggregator aggregator, UpdateModel model)
        {
            aggregator.GetEvent<UpdateLoading>().Publish(model);
        }

        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void Register(this IEventAggregator aggregator, Action<UpdateModel> action)
        {
            aggregator.GetEvent<UpdateLoading>().Subscribe(action);
        }

        /// <summary>
        /// 注册信息推送
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void RegisterMessage(this IEventAggregator aggregator, Action<string> action)
        {
            aggregator.GetEvent<StringMessageEvent>().Subscribe(action);
        }

        /// <summary>
        /// 推送提示信息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="message"></param>
        public static void SendMessage(this IEventAggregator aggregator, string message)
        {
            aggregator.GetEvent<StringMessageEvent>().Publish(message);
        }
    }
}
