using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neetsonic.DataStructure;
using Neetsonic.Tool;

namespace Neetsonic.Control
{
    /// <summary>
    /// 图片浏览器
    /// Neetsonic
    /// 2017.8.12
    /// </summary>
    public partial class PictureViewer : UserControl
    {
        private readonly CachePool<string, Image> Cache = new CachePool<string, Image>(20);
        private readonly List<string> PicFiles = new List<string>();
        private int _currentIndex = -1;

        /// <summary>
        /// 构造
        /// </summary>
        public PictureViewer() => InitializeComponent();

        private int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                OnCurrentIndexChanged();
            }
        }
        /// <summary>
        /// 缓存大小
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), Category("自定义属性"), Description("缓存大小"), DefaultValue(20)]
        public int CacheSize
        {
            get => Cache.Size;
            set => Cache.Size = value;
        }

        private async void OnCurrentIndexChanged()
        {
            void AsyncShowPicture(Image img, string page, string name) => BeginInvoke(new MethodInvoker(() =>
            {
                pic.Image = img;
                lblPage.Text = page;
                lblName.Text = name;
            }));

            await Task.Run(() =>
            {
                if(-1 == CurrentIndex)
                {
                    AsyncShowPicture(null, @"0 / 0", @"当前没有图片");
                }
                else
                {
                    string file = PicFiles[CurrentIndex];
                    string fileSHA1 = SHA1Tool.EncryptUnicode(file);
                    Image img = Cache[fileSHA1]; // 先从缓存读取，如果缓存没有，则写入缓存
                    if(null == img)
                    {
                        img = FileTool.ReadImageFile(file);
                        Cache.Add(fileSHA1, img);
                    }
                    AsyncShowPicture(img, string.Format($@"{CurrentIndex + 1} / {PicFiles.Count}"), Path.GetFileName(file));
                }
            });
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            PicFiles.Clear();
            Cache.Clear();
            CurrentIndex = -1;
        }
        /// <summary>
        /// 添加图片文件
        /// </summary>
        /// <param name="files">图片文件路径集</param>
        public void AddPicFiles(IEnumerable<string> files)
        {
            PicFiles.AddRange(files);
            if(-1 == CurrentIndex && PicFiles.Count > 0)
            {
                CurrentIndex = 0;
            }
        }
        /// <summary>
        /// 更换图片文件
        /// </summary>
        /// <param name="files">图片文件路径集</param>
        public void ChangePicFiles(IEnumerable<string> files)
        {
            Clear();
            AddPicFiles(files);
        }

        private void Pic_MouseMove(object sender, MouseEventArgs e) => pic.Cursor = e.X > pic.Width >> 1 ? Cursors.PanEast : Cursors.PanWest;
        private void Pic_MouseClick(object sender, MouseEventArgs e)
        {
            if(-1 == CurrentIndex) return;
            if(Cursors.PanWest == pic.Cursor)
            {
                CurrentIndex = (CurrentIndex - 1 + PicFiles.Count) % PicFiles.Count;
            }
            else if(Cursors.PanEast == pic.Cursor)
            {
                CurrentIndex = (CurrentIndex + 1) % PicFiles.Count;
            }
        }
    }
}