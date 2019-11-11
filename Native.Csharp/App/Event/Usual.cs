﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

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


        public void Trace_Output(string Text)
        {
            if(Trace_Enabled==true)
            {
                Common.CqApi.SendPrivateMessage(403828602, Text);
            }
            
        }//调试输出的函数
        public void Shoulie_Image()
        {

        }
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
    }
}
