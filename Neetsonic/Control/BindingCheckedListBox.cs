using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Neetsonic.DataStructure;

namespace Neetsonic.Control
{
    /// <summary>
    /// 绑定数据的CheckedListBox
    /// Neetsonic
    /// 2017.7.13
    /// </summary>
    /// <typeparam name="T">绑定数据类型</typeparam>
    public partial class BindingCheckedListBox<T> : CheckedListBox
    {
        /// <summary>
        /// 绑定的数据列表
        /// </summary>
        private BindingList<T> _dataList;
        /// <summary>
        /// 构造
        /// </summary>
        protected BindingCheckedListBox()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 数据源列表
        /// </summary>
        public BindingList<T> DataList
        {
            get => _dataList;
            set
            {
                // 记录之前的勾选项和选中项
                List<T> checkedItems = new List<T>();
                if(CheckedItems.Count > 0)
                {
                    checkedItems.AddRange(CheckedItems.Cast<T>());
                }
                T selectedItem = (T)SelectedItem;

                // 更改绑定数据
                _dataList?.Clear();
                _dataList = value;
                DataSource = _dataList;

                // 设置显示和值
                if(null == _dataList) return;
                DisplayMember = DisplayMemberName;
                ValueMember = ValueMemberName;

                // 还原之前的勾选项和选中项
                for(int idx = 0; idx < DataList.Count; idx++)
                {
                    T currItem = DataList[idx];
                    if(checkedItems.Any(item => IsTheSameItem(currItem, item)))
                        SetItemChecked(idx, true);
                    if(null != selectedItem && IsTheSameItem(currItem, selectedItem))
                        SetSelected(idx, true);
                }
            }
        }
        /// <summary>
        /// 绑定控件后的显示属性
        /// </summary>
        protected virtual string DisplayMemberName => null;
        /// <summary>
        /// 绑定控件后的值属性
        /// </summary>
        protected virtual string ValueMemberName => null;

        /// <summary>
        /// 全选
        /// </summary>
        public void CheckAll()
        {
            for(int idx = 0; idx < Items.Count; idx++) SetItemChecked(idx, true);
        }
        /// <summary>
        /// 初始化设置
        /// </summary>
        private void Init()
        {
            CheckOnClick = true;
            HorizontalScrollbar = true;
            DataList = null;
        }
        /// <summary>
        /// 判断两个项是否是同样的，用于重载绑定数据后勾选之前以前勾选中的项
        /// </summary>
        /// <param name="t1">比较项</param>
        /// <param name="t2">比较项</param>
        /// <returns>是否是相同项</returns>
        protected virtual bool IsTheSameItem(T t1, T t2) => t1.Equals(t2);
        /// <summary>
        /// 反选
        /// </summary>
        public void ReverseCheck()
        {
            for(int idx = 0; idx < Items.Count; idx++) SetItemChecked(idx, !GetItemChecked(idx));
        }
        /// <summary>
        /// 全不选
        /// </summary>
        public void UncheckAll()
        {
            for(int idx = 0; idx < Items.Count; idx++) SetItemChecked(idx, false);
        }
    }
}