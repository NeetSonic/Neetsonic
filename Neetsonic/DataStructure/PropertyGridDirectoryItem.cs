using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Neetsonic.DataStructure
{
    /// <summary>
    /// 用于PropertyGrid的可打开目录选择对话框的项目
    /// Neetsonic
    /// 2018.5.3
    /// </summary>
    public class PropertyGridDirectoryItem : UITypeEditor
    {
        /// <summary>
        /// 项目的UI类型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;
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
                    FolderBrowserDialog dlg = new FolderBrowserDialog
                    {
                        RootFolder = Environment.SpecialFolder.Desktop
                    };
                    if(DialogResult.OK == dlg.ShowDialog())
                    {
                        return dlg.SelectedPath;
                    }
                }
            }
            return value;
        }
    }
}