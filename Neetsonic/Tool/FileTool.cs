using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
            if(null == files) return;
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
        /// <summary>
        /// 创建文件，并写入文本
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="textToWrite">要写入的文本</param>
        public static void CreateAndWriteText(string filePath, string textToWrite)
        {
            using(FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                using(StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(textToWrite);
                }
            }
        }
        /// <summary>
        /// 打开文件，并读取所有文本
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件文本</returns>
        public static string OpenAndReadAllText(string filePath)
        {
            using(FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using(StreamReader reader = new StreamReader(fs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// 打开一个图片文件到内存对象（不会持续占用文件）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>图片内存对象</returns>
        public static Image ReadImageFile(string filePath)
        {
            using(FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(fs);
            }
        }
    }
}