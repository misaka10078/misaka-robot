using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using System;
using System.Linq;
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
            if(e.Message =="/功能一览")
            {
                Common.CqApi.SendGroupMessage(e.FromGroup, "催命BOT功能一览：\r\n/催命：催JK的命\r\n" +
                    "/高考倒计时：显示高考倒计时\r\n我想被禁言XX小时：自助禁言功能\r\n/图库列表：显示当前支持图库");
            }

            if(e.Message =="/更新ID")
            {
                RealUsual.Daliy_Fresh(outdt.Days);
            }
            if(e.Message=="/一言")
            {
               // Common.CqApi.SendGroupMessage(e.FromGroup, RealUsual.One_Word());
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


            foreach(string word in Usual.Image_Group_Name)
            {
                if (e.Message.Substring(1)==word)
                {
                    int GroupNum = Usual.Image_Group_Name.ToList().IndexOf(word);
                    if (Usual.Image_Group[GroupNum] > 0)
                    {
                        string Filename = RealUsual.Get_Image_Path(GroupNum);  //查找当前关键词所在的数组索引                 
                        Common.CqApi.SendGroupMessage(e.FromGroup, Common.CqApi.CqCode_Image(Filename));
                    }
                    else { Common.CqApi.SendGroupMessage(e.FromGroup, "当前图库中没有图片"); }
                }
            }

            if (e.Message =="/图库列表")
            {
                string outlist="";
                foreach(string list in Usual.Image_Group_Name)
                {outlist = outlist + list + "\r\n";}
                Common.CqApi.SendGroupMessage(e.FromGroup, "当前支持的图库列表是\r\n" + outlist+"\r\n发送/+图库名称获取对应图片");
            }

            if (e.Message.Contains ("我想被禁言"))
            {
                RealUsual.BanSpeak(e.FromGroup, e.FromQQ, e.Message);
            }
           
        }
    }
}   
