using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Neetsonic.Control
{
    /// <summary>
    /// 可选择启用的TimePicker
    /// </summary>
    public partial class CheckedDateTimePicker : UserControl
    {
        /// <summary>
        /// 构造
        /// </summary>
        public CheckedDateTimePicker()
        {
            InitializeComponent();
            InitControl();
        }

        private void InitControl()
        {
            chk.Text = CheckText;
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            date.Enabled = chk.Checked;
        }

        /// <summary>
        /// 勾选提示文本
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("勾选提示文本")]
        public string CheckText
        {
            get => chk.Text;
            set => chk.Text = value;
        }
    }
}