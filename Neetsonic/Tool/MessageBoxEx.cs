using System.Windows.Forms;

namespace Neetsonic.Tool
{
    /// <summary>
    /// 消息提示框
    /// Neetsonic
    /// 2017.6.20
    /// </summary>
    public static class MessageBoxEx
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="text">提示文本</param>
        public static DialogResult Error(string text) => MessageBox.Show(text, @"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="text">提示文本</param>
        public static DialogResult Info(string text) => MessageBox.Show(text, @"信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        /// <summary>
        /// 警告提示
        /// </summary>
        /// <param name="text">提示文本</param>
        public static DialogResult Warning(string text) => MessageBox.Show(text, @"警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        /// <summary>
        /// 请求确认提示
        /// </summary>
        /// <param name="text">提示文本</param>
        public static DialogResult Confirm(string text) => MessageBox.Show(text, @"请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
    }
}