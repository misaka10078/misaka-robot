using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp;
using System;
using System.Linq;
using System.Text.RegularExpressions;
namespace Native.Csharp.App.Event
{
    public class Event_GroupMessage : IReceiveGroupMessage
    {
        private Usual RealUsual = new Usual();
        public void ReceiveGroupMessage(object sender, CqGroupMessageEventArgs e)
        {
            
            DateTime dt = DateTime.Now;
            DateTime dtfinal = new DateTime(2020, 6, 7, 9, 0, 0);
            TimeSpan outdt = dtfinal - dt;
            
            if (Usual.Mone_ID_day > outdt.Days)
            {
                RealUsual.Daliy_Fresh(outdt.Days);
            }
            if(e.Message =="/功能一览")
            {
                Common.CqApi.SendGroupMessage(e.FromGroup, "催命BOT功能一览：\r\n/催命：催JK的命\r\n" +
                    "/高考倒计时：显示高考倒计时\r\n我想被禁言XX小时：自助禁言功能\r\n/图库列表：显示当前支持图库\r\n"
                    + "\r\n竹竹搜图BOT指令一览：\r\n竹竹搜图：之后发送图片搜索来源\r\n竹竹来张<R18><关键词>色图：搜索对应关键词的色图\r\n"+
                    "详细指令地址可见https://github.com/Tsuk1ko/CQ-picfinder-robot/wiki/%E5%A6%82%E4%BD%95%E9%A3%9F%E7%94%A8\r\n\r\n"+
                    "FF14塔塔露BOT：详见http://tataru.aoba.vip/help.php");
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



            if (Regex.IsMatch(e.Message, @"^/.*"))//此处匹配/开头的任意语句
            {
                foreach (string word in Usual.Image_Group_Name)
                {
                    if (e.Message.Substring(1) == word)//此处抠掉了第一个/进行匹配
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
            }//此处是发图库图片的请求


            

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
            if (Regex.IsMatch(e.Message, @"想要被(禁言|经验)[0-9|一]*[(小时)|(分钟)|(天)]*"))
            {
                if(e.FromQQ == 403828602)
                {
                    Int64 MemberQQ = Convert.ToInt64(RealUsual.GetAtID(e.Message));
                    //if (Regex.IsMatch(e.Message, @"小林樱"))//此处处理常见ID对应的QQ号
                    //{
                    //    MemberQQ = 414044464;
                    //}
                    //else if (Regex.IsMatch(e.Message, @"二五仔"))
                    //{ MemberQQ = 252299210; }

                    if (MemberQQ <= 0)
                    { Common.CqApi.SendGroupMessage(e.FromGroup, "获取AT信息失败"); }
                    else
                    {
                        RealUsual.BanSpeak_initiative(e.FromGroup, MemberQQ, e.Message);
                    }
                }
                else { Common.CqApi.SendGroupMessage(e.FromGroup, "¿"); }
                
            }
            //Common.CqApi.SendGroupMessage(421773783, "结果"+RealUsual.GetAtID(e.Message));


        }
    }
}   
