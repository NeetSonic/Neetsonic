using System.Collections.Generic;
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
    }
}