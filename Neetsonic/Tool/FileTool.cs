using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Neetsonic.Tool
{
    /// <summary>
    /// 文件处理工具
    /// </summary>
    public static class FileTool
    {
        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="files">文件路径集合</param>
        public static void DeleteFiles(IEnumerable<string> files)
        {
            foreach(string file in files)
            {
                File.Delete(file);
            }
        }
        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        public static void OpenDirectory(string dir)
        {
            Process.Start(@"Explorer", dir);
        }
    }
}