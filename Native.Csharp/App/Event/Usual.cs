using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using Native.Csharp.Sdk.Cqp;
using Native.Csharp.Sdk.Cqp.Model;
using Native.Csharp.Sdk.Cqp.Enum;
using System.Text.RegularExpressions;


namespace Native.Csharp.App.Event
{
    class Usual
    {
        public static DateTime Logdate;//此处记录上一次每日检查的时间，拿来对比和记录

        public static long Test_GroupID = 262787331;//MBK群
        public static long Test_MoneID = 2366325788;//moneQQ号
        //public static long Test_GroupID=421773783;//测试群
        //public static long Test_MoneID = 2824398891;//测试QQ号

        public static bool flag = false;
        public static int Change_Report_Language_Count = 0;
        public static string languagemod1_start = "我亲爱的小树苗，你可爱的小树枝提醒你：\r\n";
        public static string languagemod1_over = "\r\n时间还有很多，一起来玩耍吧~Lahee";

        public static int Mone_ID_day=300;//储存MONE的ID中的倒计时日期

        public static bool Trace_Enabled = true;

        public static int[] Image_Group;//存放图片组序号的参数
        //0 狩猎 1 药酱
        public static string[] Image_Group_Name;//存放各图片组名称的数组
        public static string Root_Path;//存放程序根目录地址
        public static bool Input_Image=false;
        public static string Image_Title = "";//存放添加图片的主题

        public void Scan_Local_Image()
        {
           
                DirectoryInfo Root_Dir = new DirectoryInfo(Root_Path + "\\data\\image\\");
                DirectoryInfo[] Dir_Name = Root_Dir.GetDirectories();//储存扫描获得的目录数组
                Image_Group_Name = new string[Root_Dir.GetDirectories().Length];//初始化图像组名称储存数组的长度
                Image_Group = new int[Root_Dir.GetDirectories().Length];//初始化图像数量数组的长度
                for (int T = 0; T < Root_Dir.GetDirectories().Length; T++)
                {
                    Image_Group_Name[T] = Dir_Name[T].ToString();//将扫描获得的文件夹名称挨个放进图像组名称储存数组中
                }
                //此段代码扫描了image目录下的所有文件夹并储存为图像组的名称

                for (int i = 0; i < Image_Group_Name.Length; i++)
                {
                    DirectoryInfo root = new DirectoryInfo(Root_Path + "\\data\\image\\" + Image_Group_Name[i]);                   
                    FileInfo[] files = root.GetFiles();                   
                    Image_Group[i] = files.Length;//读取图片数量，储存                    
                }
           
        }

        public string Get_Image_Path(int Num)
        {
           
                Random R = new Random();
                int a = R.Next(0, Image_Group[Num] - 1);
                DirectoryInfo root = new DirectoryInfo(Root_Path + "\\data\\image\\" + Image_Group_Name[Num]);
                FileInfo[] files = root.GetFiles();
                return Image_Group_Name[Num] + "\\" + files[a].Name;
            
            
        }
        public string GetAtID(string Message)
        {
            foreach (var cqMsg in CqMsg.Parse(Message).Contents)
            {
                string ID = "";
                try {  ID = cqMsg.Dictionary["qq"]; }//如果能检索到ID目录
                catch { return "-1"; }//如果qq参数内容不是空的
                if (!string.IsNullOrEmpty(ID))
                {
                    return ID;
                }
            }
            return "0";
        }//这是获取AT的QQ的函数
        public void Trace_Output(string Text)
        {
            if(Trace_Enabled==true)
            {
                Common.CqApi.SendPrivateMessage(403828602, Text);
            }
            
        }//调试输出的函数
        public void Daliy_Fresh(int Days)
        {
            
            //Common.CqApi.SendGroupMessage(Usual.Test_GroupID, sr.ReadToEnd());        
            Common.CqApi.SetGroupMemberNewCard(Usual.Test_GroupID, Usual.Test_MoneID, "极限玩耍：" + Days.ToString());
            //此处将mone的ID改为剩余高考天数
            //Usual.Logdate = Usual.Logdate.AddDays(1);//记录日期+1，以便在明天再次触发  
            //Usual.Logdate = Usual.Logdate.AddHours(-Usual.Logdate.Hour + 6);//记录小时数设置为6点，
            //就能在明天6点等待触发
            Common.CqApi.SendGroupMessage(Usual.Test_GroupID, "极限玩耍倒计时" + Days + "天，Mone的ID已经" +
                "更新，今天也要加油哦~\r\n一言：\r\n"+ One_Word());
            Mone_ID_day = Days;
            //TestObj = new Usual();
            //TestObj.Trace_Output(Usual.Logdate.ToString());
            
        }
        public string One_Word()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://v1.hitokoto.cn/?encode=text");
            WebResponse response = request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(resStream);
            string Word = sr.ReadToEnd();
            return Word;       
        }

        public void BanSpeak(long GroupID,long MemberID,String Message )
        {
            Double  GetTime = 0;
            try
            {
                String m2 = Message.Substring(0, Message.Length - 2);
                GetTime = Convert.ToDouble(m2.Substring(5));
            }
            catch
            {
                Common.CqApi.SendGroupMessage(GroupID, "禁言语法异常");
                return;
            }//判断命令格式并储存时间

            
            if (GetTime > 0&&GetTime <=720)
            {
                Double Bantime = Math.Round(GetTime * 3600);//获取最终禁言秒数并取整处理
                GroupMemberInfo Info_Check = Common.CqApi.GetMemberInfo(GroupID, MemberID);
                if (Info_Check.PermitType == PermitType.Manage|| Info_Check.PermitType == PermitType.Holder)
                {
                    Common.CqApi.SendGroupMessage(GroupID, "管理员或群主不能禁言哦~");
                    return;
                }//判断是否是管理员

                Common.CqApi.SetGroupBanSpeak(GroupID, MemberID, TimeSpan.FromSeconds(Bantime));
                Common.CqApi.SendGroupMessage(GroupID, Common.CqApi.CqCode_At(MemberID)+"已经被禁言了"+
                    GetTime.ToString() + "小时");
            }
            else if (GetTime <= 0) { Common.CqApi.SendGroupMessage(GroupID, "禁言时间不能小于等于0"); return; }
            else if (GetTime > 720) { Common.CqApi.SendGroupMessage(GroupID, "禁言时间不能大于30天"); return; }
        }

        public void BanSpeak_initiative(long GroupID, long MemberID, String Message)
        {
            Double GetTime=0;//时间基数（分钟
            int mult=0;//单位倍率
            try
            {
                //Common.CqApi.SendGroupMessage(GroupID, "进入处理");
                string PreOrder = Regex.Match(Message, @"被(禁言|经验)[0-9|一]*[(小时)|(分钟)|(天)]*").ToString();
                //Common.CqApi.SendGroupMessage(GroupID, PreOrder);
                if (PreOrder.Substring(3, 1) == "一")
                { GetTime = 1; }
                else {GetTime = Convert.ToDouble(Regex.Match(Message, @"(禁言|经验)[0-9|]*").ToString().Substring(2)); }//获取时间值 
                    string multWord = Regex.Match(Message, @"(小时)|(分钟)|天").ToString();//获取时间单位
                    if (multWord == "分钟")
                    { mult = 1; }
                    else if (multWord == "小时")
                    { mult = 60; }
                    else if (multWord == "天")
                    { mult = 1440; }
                    else { Common.CqApi.SendGroupMessage(GroupID, "时间单位获取失败，请检查"); }
            }
            catch
            {
                Common.CqApi.SendGroupMessage(GroupID, "禁言语法异常");
                return;
            }
            Double FinallTime = mult * GetTime * 60;//获取最终禁言秒数并取整处理


            if (FinallTime > 0 && FinallTime <= 62208000)
            {
                GroupMemberInfo Info_Check = Common.CqApi.GetMemberInfo(GroupID, MemberID);
                if (Info_Check.PermitType == PermitType.Manage|| Info_Check.PermitType == PermitType.Holder)
                {
                    Common.CqApi.SendGroupMessage(GroupID, "管理员或群主不能禁言哦~");
                    return;
                }//判断是否是管理员
                Common.CqApi.SetGroupBanSpeak(GroupID, MemberID, TimeSpan.FromSeconds(FinallTime));
                Common.CqApi.SendGroupMessage(GroupID, Common.CqApi.CqCode_At(MemberID)+"有罪！处刑！");
            }
            else if (FinallTime <= 0) { Common.CqApi.SendGroupMessage(GroupID, "禁言时间不能小于等于0"); return; }
            else if (FinallTime > 62208000) { Common.CqApi.SendGroupMessage(GroupID, "禁言时间不能大于30天"); return; }
            else { Common.CqApi.SendGroupMessage(GroupID, "发生未知错误"); }
            
        }

    }
}
