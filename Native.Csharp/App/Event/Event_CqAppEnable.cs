using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Csharp.Sdk.Cqp.EventArgs;
using Native.Csharp.Sdk.Cqp.Interface;
using Native.Csharp.Sdk.Cqp.Model;


namespace Native.Csharp.App.Event
{
    /// <summary>
	/// Type=1003 应用被启用, 事件实现
	/// </summary>
    public class Event_CqAppEnable : ICqAppEnable
    {
        /// <summary>
		/// 处理 酷Q 的插件启动事件回调
		/// </summary>
		/// <param name="sender">事件的触发对象</param>
		/// <param name="e">事件的附加参数</param>
        private Usual  classObj;
        public void CqAppEnable (object sender, CqAppEnableEventArgs e)
        {
            // 当应用被启用后，本方法将被调用。
            // 如果酷Q载入时应用已被启用，则在 ICqStartup 接口的实现方法被调用后，本方法也将被调用一次。
            // 如非必要，不建议在这里加载窗口。（可以添加菜单，让用户手动打开窗口）
            
            Usual.Logdate= DateTime.Now;//初始化时记录当前时间
            Usual.Logdate = Usual.Logdate.AddDays(1);
            Usual.Logdate=Usual.Logdate.AddHours(-Usual.Logdate.Hour + 6);
            classObj = new Usual() ;
            classObj.Trace_Output(Usual.Logdate.ToString());

            GroupMemberInfo member = Common.CqApi.GetMemberInfo(262787331, 2366325788);
            string txt = member.Nick.Substring(member.Nick.Length - 3);
            Usual.Mone_ID_day = Convert.ToInt32(txt);//获取Mone的ID并截取后3位转化成日期



            Common.IsRunning = true;
        }
    }
}
