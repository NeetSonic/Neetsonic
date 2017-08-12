using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Neetsonic.Control
{
    /// <summary>
    /// 可选择启用的TimePicker
    /// Neetsonic
    /// 2017.8.10
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

        /// <summary>
        /// 获取或设置时间值，null表示未启用
        /// </summary>
        public DateTime? Value
        {
            get => chk.Checked ? date.Value : (DateTime?)null;
            set
            {
                if(null == value)
                {
                    chk.Checked = false;
                }
                else
                {
                    chk.Checked = true;
                    date.Value = value.Value;
                }
            }
        }

        /// <summary>
        /// 勾选提示文本
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("自定义属性"), Description("勾选提示文本"), DefaultValue(null)]
        public string CheckText
        {
            get => chk.Text;
            set => chk.Text = value;
        }

        /// <summary>
        /// 勾选状态
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("自定义属性"), Description("勾选状态"), DefaultValue(false)]
        public bool Checked
        {
            get => chk.Checked;
            set => chk.Checked = value;
        }

        /// <summary>
        /// 日期格式
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("自定义属性"), Description("日期格式"), DefaultValue(DateTimePickerFormat.Long)]
        public DateTimePickerFormat DateFormat
        {
            get => date.Format;
            set => date.Format = value;
        }

        /// <summary>
        /// 自定义日期格式
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("自定义属性"), Description("自定义日期格式"), DefaultValue(null)]
        public string CustomDateFormat
        {
            get => date.CustomFormat;
            set => date.CustomFormat = value;
        }

        private void InitControl()
        {
            chk.Checked = false;
            chk.Text = null;
            date.Enabled = false;
            date.Format = DateTimePickerFormat.Long;
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            date.Enabled = chk.Checked;
        }
    }
}