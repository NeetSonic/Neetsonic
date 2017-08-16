using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Neetsonic.Control
{
    /// <summary>
    /// 可定制的GroupBox
    /// 1.标题字体颜色
    /// 2.边框颜色
    /// Neetsonic
    /// 2017.8.10
    /// </summary>
    public partial class GroupBox : System.Windows.Forms.GroupBox
    {
        /// <summary>
        /// 构造
        /// </summary>
        public GroupBox()
        {
            InitializeComponent();
        }

        private Color _borderColor = Color.Black;
        private Color _textColor = Color.Black;

        /// <summary>
        /// 标题字体颜色
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Description("标题字体颜色"), Category("外观"), DefaultValue(typeof(Color), @"Black")]
        public Color TextColor
        {
            get => _textColor;
            set => _textColor = value;
        }
        /// <summary>
        /// 边框颜色
        /// </summary>        
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Description("边框颜色"), Category("外观"), DefaultValue(typeof(Color), @"Black")]
        public Color BorderColor
        {
            get => _borderColor;
            set => _borderColor = value;
        }

        /// <summary>
        /// 重绘UI
        /// </summary>
        /// <param name="e">绘制参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Size fontSize = e.Graphics.MeasureString(Text, Font).ToSize();
            const int padding = 1;
            const int widthBegin = 10;
            int heightBegin = fontSize.Height >> 1;
            e.Graphics.Clear(BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            using(Brush brush = new SolidBrush(TextColor))
            {
                e.Graphics.DrawString(Text, Font, brush, widthBegin + padding, 0);
            }
            using(Pen borderPen = new Pen(BorderColor))
            {
                e.Graphics.DrawLine(borderPen, 0, heightBegin, widthBegin, heightBegin);
                e.Graphics.DrawLine(borderPen, fontSize.Width + widthBegin - padding, heightBegin, Width - padding, heightBegin);
                e.Graphics.DrawLine(borderPen, 0, heightBegin, 0, Height - padding);
                e.Graphics.DrawLine(borderPen, 0, Height - padding, Width - padding, Height - padding);
                e.Graphics.DrawLine(borderPen, Width - padding, heightBegin, Width - padding, Height - padding);
            }
        }
    }
}