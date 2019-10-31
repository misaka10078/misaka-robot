using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;


using System;
namespace Native.Csharp.App.Event
{
    public class Event_GroupMessage : IReceiveGroupMessage
    {
        public void ReceiveGroupMessage(object sender, CqGroupMessageEventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime dtfinal = new DateTime(2020, 6, 7, 0, 0, 0);
            TimeSpan outdt = dtfinal - dt;
            if (Usual.Logdate<DateTime.Now)
            {
                Common.CqApi.SetGroupMemberNewCard(Usual.Test_GroupID , Usual.Test_MoneID , outdt.Days.ToString());
                //此处将mone的ID改为剩余高考天数
                Usual.Logdate = Usual.Logdate.AddDays(1);
                Usual.Logdate = Usual.Logdate.AddSeconds(1);//记录日期+1，以便在明天再次触发  
                Common.CqApi.SendGroupMessage(Usual.Test_GroupID, "Mone的ID已经更新，今天也要加油哦~");
            }

            if (e.Message == "/催命")
            {
                var outputmessage = "距离2020高考还有" + outdt.Days + "天" +
                outdt.Hours + "小时" + outdt.Minutes + "分钟" + outdt.Seconds + "秒";
                Common.CqApi.SendGroupMessage(e.FromGroup, Common.CqApi.CqCode_At(2366325788, true) + Usual.languagemod1_start
                    + outputmessage + Usual.languagemod1_over);
            }

            if (e.Message == "高考倒计时")
            {           
                var outputmessage = "距离2020高考还有" + outdt.Days + "天" +
                outdt.Hours + "小时" + outdt.Minutes + "分钟" + outdt.Seconds + "秒";
                //long QQID = 2366325788;
                Common.CqApi.SendGroupMessage(e.FromGroup, Common.CqApi.CqCode_At(e.FromQQ, true) + Usual.languagemod1_start
                    + outputmessage+ Usual.languagemod1_over);
            }
        }
    }
}   
