using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp;
using System;
using System.IO;
using System.Linq;

namespace Native.Csharp.App.Event
{
    public class Event_FriendMessage : IReceiveFriendMessage
    {
        private Usual TestObj;
        
        public void ReceiveFriendMessage(object sender, CqPrivateMessageEventArgs e)
        {
            TestObj = new Usual();

            if (e.Message == "更新报时语言" && e.FromQQ == 403828602)
            {
                Usual.Change_Report_Language_Count = 2;
                Common.CqApi.SendPrivateMessage(e.FromQQ, "请开始输入两个报时语言，开头和结尾，分两次发送");

                

            }
            else if (Usual.Change_Report_Language_Count == 2 && e.FromQQ == 403828602)
            {
                Usual.Change_Report_Language_Count--;
                Usual.languagemod1_start = e.Message + "\r\n";
                Common.CqApi.SendPrivateMessage(e.FromQQ, "开头输入成功，请继续输入");
            }
            else if (Usual.Change_Report_Language_Count == 1 && e.FromQQ == 403828602)
            {
                Usual.Change_Report_Language_Count--;
                Usual.languagemod1_over = "\r\n" + e.Message;
                Common.CqApi.SendPrivateMessage(e.FromQQ, "报时语言更新成功");
            }

            if (e.Message == "/切换调试模式" && e.FromQQ == 403828602)
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

            if (e.Message.Contains("/添加图片"))
            {
                string FullList = "";
                foreach (string list in Usual.Image_Group_Name)
                {
                    FullList = FullList + list + "\r\n";
                }
                if(FullList.Contains(e.Message.Substring(6)))
                {
                    Usual.Input_Image = true;
                    Usual.Image_Title = e.Message.Substring(6);
                    Common.CqApi.SendPrivateMessage(e.FromQQ, "发送图片导入主题："+ e.Message.Substring(6)+
                        "\r\n完毕后输入“/完成”");
                }
                else { Common.CqApi.SendPrivateMessage(e.FromQQ, "不存在这个主题，请检查"); }
            }

            if (e.Message.Contains("/添加主题"))
            {
                
                Array.Resize(ref Usual.Image_Group_Name, Usual.Image_Group_Name.Length +1);//名字数组加1
                Array.Resize(ref Usual.Image_Group, Usual.Image_Group.Length + 1);//图片统计数组加1
                Usual.Image_Group_Name[Usual.Image_Group_Name.Length - 1] = e.Message.Substring(6);
                Usual.Image_Group[Usual.Image_Group.Length - 1] = 0;

                Directory.CreateDirectory(Usual.Root_Path + "\\data\\image\\" + e.Message.Substring(6));
                Common.CqApi.SendPrivateMessage(e.FromQQ, "成功导入主题：" + e.Message.Substring(6));
            }

            if (e.Message.Contains("/完成")&& Usual.Input_Image == true)
            {
                Usual.Input_Image = false;
                Usual.Image_Title = "";
                Common.CqApi.SendPrivateMessage(e.FromQQ, "图片导入结束");
            }

                if (Usual.Input_Image == true)
            {
                foreach (var cqMsg in CqMsg.Parse(e.Message).Contents)
                {
                    string file = cqMsg.Dictionary["file"];
                    //如果“file”参数内容不是空的
                    if (!string.IsNullOrEmpty(file))
                    {
                        //使用API将“cqimg”文件转换成图片文件，并返回图片文件路径
                        try
                        {
                            string fileName = Common.CqApi.ReceiveImage(file);
                        FileInfo Files = new FileInfo(fileName);
                        DirectoryInfo 目的 = new DirectoryInfo(Usual.Root_Path + "\\data\\image\\" + Usual.Image_Title);
                         Files.MoveTo(目的 + "\\" + Path.GetFileName(file));


                            int GroupNum = Usual.Image_Group_Name.ToList().IndexOf(Usual.Image_Title);
                            Usual.Image_Group[GroupNum]++;
                            Common.CqApi.SendPrivateMessage(e.FromQQ, "图片导入成功");


                        }
                        catch 
                        { 
                            Common.CqApi.SendPrivateMessage(e.FromQQ, "图片导入失败");
                            TestObj.Trace_Output("接收到的数据是"+ cqMsg.Dictionary["file"]);
                        }
                       
                        
                        
                    }
                }


               

                //Common.CqApi.SendPrivateMessage(e.FromQQ, e.Message.ToString());
                //MessageBox.Show(e.Message.ToString());

            }
        }
    }
}