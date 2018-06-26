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
        /// 批量删除文件
        /// </summary>
        /// <param name="files">文件路径集合</param>
        public static void DeleteFiles(IEnumerable<string> files)
        {
            if(files is null) return;
            foreach(string file in files) File.Delete(file);
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
        /// 打开文件夹
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        public static void OpenDirectory(string dir) => Process.Start(@"Explorer", dir);
        /// <summary>
        /// 打开文本文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void OpenTextFile(string filePath) => Process.Start(@"Notepad", filePath);
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
        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="newName">新文件名</param>
        /// <returns>新的文件路径</returns>
        public static string Rename(string filePath, string newName)
        {
            string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newName);
            File.Move(filePath, newFilePath);
            return newFilePath;
        }
        /// <summary>
        /// 将文件名合法化，替换不合法的符号
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>合法的文件名</returns>
        public static string LegalizeFileName(string fileName)
        {
            fileName = fileName.Replace('/', '／');
            fileName = fileName.Replace('\\', '＼');
            fileName = fileName.Replace(':', '：');
            fileName = fileName.Replace(':', '：');
            fileName = fileName.Replace('*', '※');
            fileName = fileName.Replace('?', '？');
            fileName = fileName.Replace('?', '？');
            fileName = fileName.Replace('"', '“');
            fileName = fileName.Replace('<', '《');
            fileName = fileName.Replace('>', '》');
            return fileName.Replace('|', '丨');
        }
        /// <summary>
        /// 删除整个文件夹（包括只读文件）
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        private static void DeleteDir(string dir)
        {
            if(Directory.Exists(dir))
            {
                foreach(string childName in Directory.EnumerateFileSystemEntries(dir))
                {
                    if(File.Exists(childName))
                    {
                        FileInfo fi = new FileInfo(childName);
                        if(fi.IsReadOnly) fi.IsReadOnly = false;
                        File.Delete(childName);
                    }
                    else DeleteDir(childName);
                }
                Directory.Delete(dir, true);
            }
        }
    }
}