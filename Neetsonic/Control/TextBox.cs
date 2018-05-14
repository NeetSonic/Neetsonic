using System;

namespace Neetsonic.Control
{
    /// <summary>
    /// 文本框
    /// </summary>
    public partial class TextBox : System.Windows.Forms.TextBox
    {
        /// <summary>
        /// 构造
        /// </summary>
        public TextBox()
        {
            InitializeComponent();
            BindEvents();
        }

        /// <summary>
        /// 高亮文本
        /// </summary>
        public void Highlight()
        {
            Focus();
            SelectAll();
        }
        /// <summary>
        /// 文本末尾添加文本后换行
        /// </summary>
        /// <param name="txt">要添加的文本</param>
        public void AppendLine(string txt)
        {
            AppendText(txt);
            AppendText(Environment.NewLine);
        }
        private void BindEvents()
        {
            // Ctrl + A 全选
            KeyPress += (sender, e) =>
            {
                if(e.KeyChar == '\u0001')
                {
                    ((TextBox)sender).SelectAll();
                    e.Handled = true;
                }
            };
        }
    }
}