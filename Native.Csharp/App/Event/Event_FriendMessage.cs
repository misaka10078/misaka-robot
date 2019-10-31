﻿using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;


using System;
namespace Native.Csharp.App.Event
{
    public class Event_FriendMessage : IReceiveFriendMessage
    {
        public void ReceiveFriendMessage(object sender,CqPrivateMessageEventArgs e)
        {
            if(e.Message=="更新报时语言")
            {
                Usual.Change_Report_Language_Count = 2;
                Common.CqApi.SendPrivateMessage(e.FromQQ, "请开始输入两个报时语言，开头和结尾，分两次发送");
                
            }
            if(Usual.Change_Report_Language_Count == 2)
            {
                Usual.Change_Report_Language_Count = Usual.Change_Report_Language_Count--;
                Usual.languagemod1_start = e.Message + "\r\n";
                Common.CqApi.SendPrivateMessage(e.FromQQ, "开头输入成功，请继续输入");
            }
            if (Usual.Change_Report_Language_Count == 1)
            {
                Usual.Change_Report_Language_Count = Usual.Change_Report_Language_Count--;
                Usual.languagemod1_over = "\r\n"+ e.Message  ;
                Common.CqApi.SendPrivateMessage(e.FromQQ, "报时语言更新成功");
            }
        }
    }
}