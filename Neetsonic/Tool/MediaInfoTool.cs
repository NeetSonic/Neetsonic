using System.Drawing;
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
            MediaFile file = new MediaFile(filePath);
            return 0 < file.Image.Count && 0 == file.Audio.Count && 0 == file.Video.Count;
        }
        /// <summary>
        /// 判断文件是否为视频文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>判断结果</returns>
        public static bool IsVideo(string filePath)
        {
            MediaFile file = new MediaFile(filePath);
            return 0 < file.Video.Count;
        }
        /// <summary>
        /// 获取视频文件的分辨率
        /// </summary>
        /// <param name="videoFilePath">视频文件路径</param>
        /// <returns>分辨率结构</returns>
        public static Size GetVideoRes(string videoFilePath)
        {
            MediaFile file = new MediaFile(videoFilePath);
            return new Size(file.Video[0].Width, file.Video[0].Height);
        }
    }
}