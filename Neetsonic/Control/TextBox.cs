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
        }

        /// <summary>
        /// 高亮文本
        /// </summary>
        public void Highlight()
        {
            Focus();
            SelectAll();
        }
    }
}