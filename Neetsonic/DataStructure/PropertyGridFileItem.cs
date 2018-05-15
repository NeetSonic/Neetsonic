using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Neetsonic.DataStructure
{
    /// <summary>
    /// 用于PropertyGrid的可打开文件选择对话框的项目
    /// Neetsonic
    /// 2018.5.15
    /// </summary>
    public class PropertyGridFileItem : UITypeEditor
    {
        /// <summary>
        /// 项目的值的修改
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if(provider != null)
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if(edSvc != null)
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    if(DialogResult.OK == dlg.ShowDialog())
                    {
                        return dlg.FileName;
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// 项目的UI类型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;
    }
}