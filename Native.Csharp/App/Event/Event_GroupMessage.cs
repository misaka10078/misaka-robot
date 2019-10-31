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
            if (dt.Hour>7 & dt.Hour < 12)
            {

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
