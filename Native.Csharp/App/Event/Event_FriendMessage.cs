using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp;

using System.Windows.Forms;

namespace Native.Csharp.App.Event
{
    public class Event_FriendMessage : IReceiveFriendMessage
    {
        private Usual TestObj;
        public void ReceiveFriendMessage(object sender,CqPrivateMessageEventArgs e)
        {
            TestObj = new Usual();
            
            if (e.Message=="更新报时语言" && e.FromQQ == 403828602)
            {
                Usual.Change_Report_Language_Count = 2;
                Common.CqApi.SendPrivateMessage(e.FromQQ, "请开始输入两个报时语言，开头和结尾，分两次发送");
                
                TestObj.Trace_Output(e.FromQQ.ToString());

            }
            else if(Usual.Change_Report_Language_Count == 2&& e.FromQQ==403828602)
            {
                Usual.Change_Report_Language_Count--;
                Usual.languagemod1_start = e.Message + "\r\n";
                Common.CqApi.SendPrivateMessage(e.FromQQ, "开头输入成功，请继续输入");
            }
            else if (Usual.Change_Report_Language_Count == 1 && e.FromQQ == 403828602)
            {
                Usual.Change_Report_Language_Count--;
                Usual.languagemod1_over = "\r\n"+ e.Message  ;
                Common.CqApi.SendPrivateMessage(e.FromQQ, "报时语言更新成功");
            }

            if (e.Message =="/切换调试模式" && e.FromQQ == 403828602)
            {
                Usual.Trace_Enabled = !Usual.Trace_Enabled;
                if (Usual.Trace_Enabled)
                {
                    Common.CqApi.SendPrivateMessage(e.FromQQ, "调试模式已开启");
                }
                else
                {
                    Common.CqApi.SendPrivateMessage(e.FromQQ, "调试模式已关闭");
                }
            }
            if (e.FromQQ == 403828602)
            {
                foreach (var cqMsg in CqMsg.Parse(e.Message).Contents)
                {
                    //取出CQ码中“file”参数的内容
                    string file = cqMsg.Dictionary["file"];

                    //如果“file”参数内容不是空的
                    if (!string.IsNullOrEmpty(file))
                    {
                        //使用API将“cqimg”文件转换成图片文件，并返回图片文件路径
                        string fileName = Common.CqApi.ReceiveImage(file);

                        //将图片路径发送到群内
                        Common.CqApi.SendGroupMessage(403828602, fileName);
                    }
                }     
                
            }
            //Common.CqApi.SendPrivateMessage(e.FromQQ, e.Message.ToString());
            //MessageBox.Show(e.Message.ToString());

        }
    }
}