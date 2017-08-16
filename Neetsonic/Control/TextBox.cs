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