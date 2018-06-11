using System;
using System.Text;
using System.Windows.Forms;

namespace Neetsonic.Control
{
    /// <summary>
    /// 日志文本框
    /// Neetsonic
    /// 2017.7.13
    /// </summary>
    public partial class LogTextBox : TextBox
    {
        /// <summary>
        /// 构造
        /// </summary>
        public LogTextBox()
        {
            Init();
            InitializeComponent();
        }

        /// <summary>
        /// 输出一条日志信息，并自动滚屏到最新添加的信息处
        /// </summary>
        /// <param name="log">需要输出的信息</param>
        public void WriteLog(string log)
        {
            StringBuilder sb = new StringBuilder();
            if(Text.Length > 0) sb.AppendLine();
            sb.Append(DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss"))
              .Append(@"   ")
              .AppendLine(log);
            AppendText(sb.ToString());
            ScrollToCaret();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            AcceptsReturn = true;
            AcceptsTab = true;
            Multiline = true;
            ScrollBars = ScrollBars.Both;
            WordWrap = false;
        }
    }
}