using System.Drawing;
using System.IO;
using MediaInfoDotNet;

namespace Neetsonic.Tool
{
    /// <summary>
    /// 媒体文件工具
    /// Neetsonic
    /// 2017.7.12
    /// </summary>
    public static class MediaInfoTool
    {
        /// <summary>
        /// 判断文件是否为图片文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>判断结果</returns>
        public static bool IsImage(string filePath)
        {
            MediaFile file = NewMediaFile(filePath);
            return 0 < file.Image.Count && 0 == file.Audio.Count && 0 == file.Video.Count;
        }
        /// <summary>
        /// 判断文件是否为视频文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>判断结果</returns>
        public static bool IsVideo(string filePath)
        {
            MediaFile file = NewMediaFile(filePath);
            return 0 < file.Video.Count;
        }
        /// <summary>
        /// 获取视频文件的分辨率
        /// </summary>
        /// <param name="videoFilePath">视频文件路径</param>
        /// <returns>分辨率结构</returns>
        public static Size GetVideoRes(string videoFilePath)
        {
            MediaFile file = NewMediaFile(videoFilePath);
            return new Size(file.Video[0].Width, file.Video[0].Height);
        }
        /// <summary>
        /// 创建临时文件
        /// </summary>
        /// <param name="filePath">源文件路径</param>
        /// <returns>临时文件路径</returns>
        private static string CreateTempFile(string filePath)
        {
            const string tempName = @"Temp";
            string dir = Path.GetDirectoryName(filePath);
            string tempPath = Path.Combine(dir, tempName);
            int count = 0;
            while(File.Exists(tempPath))
            {
                tempPath = Path.Combine(dir, string.Format($@"{tempName}{++count}"));
            }
            return tempPath;
        }
        /// <summary>
        /// 使用ANSI编码的临时文件路径来对接MediaFile类构造接口
        /// 因为部分UNICODE字符可能导致文件无法识别
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>构造的MediaFile对象</returns>
        private static MediaFile NewMediaFile(string filePath)
        {
            string tempFile = CreateTempFile(filePath); 
            File.Move(filePath, tempFile);
            MediaFile file = new MediaFile(tempFile);
            File.Move(tempFile, filePath);
            return file;
        }
    }
}