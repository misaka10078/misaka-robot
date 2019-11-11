using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using System;
namespace Native.Csharp.App.Event
{
    public class Event_GroupMessage : IReceiveGroupMessage
    {
        private Usual RealUsual = new Usual();
        public void ReceiveGroupMessage(object sender, CqGroupMessageEventArgs e)
        {
            
            DateTime dt = DateTime.Now;
            DateTime dtfinal = new DateTime(2020, 6, 7, 0, 0, 0);
            TimeSpan outdt = dtfinal - dt;
            //
            if (Usual.Mone_ID_day > outdt.Days)
            {
                RealUsual.Daliy_Fresh(outdt.Days);
            }

            if(e.Message =="/更新ID")
            {
                RealUsual.Daliy_Fresh(outdt.Days);
            }
            if(e.Message=="/一言")
            {
                Common.CqApi.SendGroupMessage(e.FromGroup, RealUsual.One_Word());
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
