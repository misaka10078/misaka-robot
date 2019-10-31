using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Native.Csharp.App.Event
{
    class Usual
    {
        public static DateTime Logdate;//此处记录上一次每日检查的时间，拿来对比和记录
        public static long Test_GroupID = 262787331;//MBK群
        //public static long Test_GroupID=421773783;//测试群
        public static long Test_MoneID = 2366325788;//moneQQ号
        //public static long Test_MoneID = 2824398891;//测试QQ号
        public static bool flag = false;
        public static int Change_Report_Language_Count = 0;
        public static string languagemod1_start = "我亲爱的小树苗，你可爱的小树枝提醒你：\r\n";
        public static string languagemod1_over = "\r\n时间还有很多，一起来玩耍吧~Lahee";



        public void Trace_Output(string Text)
        {
            //Common.CqApi.SendPrivateMessage(403828602, Text);
        }

        
    }
}
