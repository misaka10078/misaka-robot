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
        public static long Test_GroupID=421773783;
        public static long Test_MoneID = 2824398891;


        public static bool flag = false;
        public static int Change_Report_Language_Count = 0;
        public static string languagemod1_start = "您的小树枝提醒您：\r\n";
        public static string languagemod1_over = "\r\n时间还很多，先一起来玩吧~";
    }
}
