using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using System;
namespace Native.Csharp.App.Event
{
    public class Event_GroupMessage : IReceiveGroupMessage
    {
        private Usual TestObj;
        public void ReceiveGroupMessage(object sender, CqGroupMessageEventArgs e)
        {
            DateTime dt = DateTime.Now;
            DateTime dtfinal = new DateTime(2020, 6, 7, 9, 0, 0);
            TimeSpan outdt = dtfinal - dt;
            if (Usual.Mone_ID_day <outdt.Days)
            {
                Common.CqApi.SetGroupMemberNewCard(Usual.Test_GroupID , Usual.Test_MoneID , "极限玩耍："+outdt.Days.ToString());
                //此处将mone的ID改为剩余高考天数
                //Usual.Logdate = Usual.Logdate.AddDays(1);//记录日期+1，以便在明天再次触发  
                //Usual.Logdate = Usual.Logdate.AddHours(-Usual.Logdate.Hour + 6);//记录小时数设置为6点，就能在明天6点等待触发
                Common.CqApi.SendGroupMessage(Usual.Test_GroupID, "极限玩耍倒计时"+ outdt.Days + "天，Mone的ID已经更新，今天也要加油哦~");
                //TestObj = new Usual();
                //TestObj.Trace_Output(Usual.Logdate.ToString());
            }

            if (e.Message == "/催命")
            {
                var outputmessage = "距离2020高考还有" + outdt.Days + "天" +
                outdt.Hours + "小时" + outdt.Minutes + "分钟" + outdt.Seconds + "秒";
                Common.CqApi.SendGroupMessage(e.FromGroup, Common.CqApi.CqCode_At(2366325788, true) + Usual.languagemod1_start
                    + outputmessage + Usual.languagemod1_over);
            }

            if (e.Message == "/高考倒计时")
            {           
                var outputmessage = "距离2020高考还有" + outdt.Days + "天" +
                outdt.Hours + "小时" + outdt.Minutes + "分钟" + outdt.Seconds + "秒";
                //long QQID = 2366325788;
                Common.CqApi.SendGroupMessage(e.FromGroup, Common.CqApi.CqCode_At(e.FromQQ, true) + Usual.languagemod1_start
                    + outputmessage+ Usual.languagemod1_over);
            }

            //if(e.Message =="/狩猎")
            //{   

               // Common.CqApi.SendGroupMessage(e.FromGroup, Common.CqApi.CqCode_Image("狩猎\\1.jpg"));
           // }
        }
    }
}   
