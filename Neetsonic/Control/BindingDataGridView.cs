using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Neetsonic.Control
{
    /// <summary>
    /// 绑定数据源的DataGridView
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BindingDataGridView<T> : DataGridView
    {
        /// <summary>
        /// 构造器
        /// </summary>
        protected BindingDataGridView()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 用于更新列表中元素的委托
        /// </summary>
        /// <param name="element">要更新的元素</param>
        public delegate void UpdateElementEvent(ref T element);

        private DataStructure.BindingList<T> _dataList; // 绑定的数据源列表
        private readonly List<string> LinkColumns = new List<string>(); // 链接类型的列

        /// <summary>
        /// 对于指定为LinkColumn的单元格内容，将其视为网址，点击后自动在浏览器打开
        /// </summary>
        [Browsable(true)]
        [Description("对于指定为LinkColumn的单元格内容，将其视为网址，点击后自动在浏览器打开")]
        [Category("自定义属性")]
        public bool OpenLinkInBrowser{ get; set; }

        /// <summary>
        /// 第一个选中的项
        /// </summary>
        public T SelectedItem => SelectedRows.Count > 0 ? DataList[SelectedRows[0].Index] : default(T);

        /// <summary>
        /// 第一个选中的项索引
        /// </summary>
        public int SelectedItemIndex => SelectedRows.Count > 0 ? SelectedRows[0].Index : -1;

        /// <summary>
        /// 绑定的数据源列表
        /// </summary>
        public DataStructure.BindingList<T> DataList
        {
            get => _dataList;
            set
            {
                // 记录原先的列表排序规则和选中项
                DataGridViewColumn sortedColumn = SortedColumn;
                SortOrder oldSortOrder = SortOrder;
                T oldSelectedItem = SelectedItem;

                // 更改绑定数据
                _dataList?.Clear();
                _dataList = value;
                DataSource = _dataList;

                // 排序
                Sort(sortedColumn, oldSortOrder);

                // 还原选中项
                Type type = typeof(T);
                if(type.IsValueType)
                {
                    if(default(T).Equals(oldSelectedItem)) return;
                }
                else if(null == oldSelectedItem) return;
                int idx = DataList.FindItemIndex(item => IsTheSameItem(item, oldSelectedItem));
                if(-1 != idx)
                {
                    ClearSelection();
                    DataGridViewRow theRow = Rows[idx];
                    theRow.Selected = true;
                    if(!theRow.Displayed)
                    {
                        FirstDisplayedScrollingRowIndex = idx;
                    }
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //
            // 属性
            //
            DataList = null;
            AllowUserToResizeRows = AllowUserToDeleteRows = AllowUserToAddRows = false;
            AllowUserToResizeColumns = AllowUserToOrderColumns = false;
            RowHeadersVisible = false;
            MultiSelect = false;
            ReadOnly = true;
            BackgroundColor = Color.White;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //
            // 事件
            //
            CellContentClick += (sender, args) =>
            {
                if(OpenLinkInBrowser && -1 != args.RowIndex && LinkColumns.Contains(Columns[args.ColumnIndex].Name))
                {
                    Process.Start(@"explorer.exe", CurrentCell.Value?.ToString());
                }
            };
        }

        /// <summary>
        /// 判断两个项是否是同样的，用于重载绑定数据后勾选之前以前勾选中的项
        /// </summary>
        /// <param name="t1">比较项1</param>
        /// <param name="t2">比较项2</param>
        /// <returns>是否是相同项</returns>
        protected virtual bool IsTheSameItem(T t1, T t2)
        {
            return t1.Equals(t2);
        }

        /// <summary>
        /// 设置列
        /// </summary>
        /// <param name="columns">列信息集合</param>
        protected void SetColumns(IEnumerable<BindingDataGridViewColumn> columns)
        {
            Columns.Clear();
            LinkColumns.Clear();
            foreach(BindingDataGridViewColumn column in columns)
            {
                dynamic col = column.Type.Assembly.CreateInstance(column.Type.FullName);
                col.Name = column.NameAndDataPropertyName;
                col.DataPropertyName = column.NameAndDataPropertyName;
                col.HeaderText = column.HeaderText;
                col.Visible = column.Visible;
                col.SortMode = column.SortMode;
                Columns.Add(col);
                if(typeof(DataGridViewLinkColumn) == column.Type)
                {
                    LinkColumns.Add(col.Name);
                }
            }
        }

        /// <summary>
        /// 重新按照当前设置排序并刷新显示，且定位到刷新前选中的那一行
        /// </summary>
        public override void Refresh()
        {
            // 先确定原来选中的项，按当前排序规则重排
            T oldSelectedItem = SelectedItem;
            if(!Sort(SortedColumn, SortOrder)) return;
            Type type = typeof(T);
            if(type.IsValueType)
            {
                if(default(T).Equals(oldSelectedItem)) return;
            }
            else if(null == oldSelectedItem) return;
            int idx = DataList.FindItemIndex(item => item.Equals(oldSelectedItem));
            if(-1 != idx)
            {
                ClearSelection();
                DataGridViewRow theRow = Rows[idx];
                theRow.Selected = true;
                if(!theRow.Displayed)
                {
                    FirstDisplayedScrollingRowIndex = idx;
                }
            }
            base.Refresh();
        }

        /// <summary>
        /// 添加新项，并自动按照当前排序规则整理列表，之后定位到该项
        /// </summary>
        /// <param name="t">要添加的新项</param>
        public void AddItem(T t)
        {
            DataList.Add(t);
            ClearSelection();
            Rows[Rows.Count - 1].Selected = true;
            Refresh();
        }

        /// <summary>
        /// 移除当前选中元素
        /// </summary>
        public void RemoveCurrSelectedItem()
        {
            RemoveItemAt(SelectedItemIndex);
        }

        /// <summary>
        /// 移除项，并自动调整列宽
        /// </summary>
        /// <param name="idx">项在列表中的索引</param>
        public void RemoveItemAt(int idx)
        {
            DataList.RemoveAt(idx);
        }

        /// <summary>
        /// 按照指定规则排序
        /// </summary>
        /// <param name="column">排序的列</param>
        /// <param name="order">排序顺序</param>
        /// <returns>排序是否执行</returns>
        protected virtual bool Sort(DataGridViewColumn column, SortOrder order)
        {
            if(null == column)
            {
                return false;
            }
            ListSortDirection dir;
            switch(order)
            {
                case SortOrder.Ascending:
                {
                    dir = ListSortDirection.Ascending;
                    break;
                }
                case SortOrder.Descending:
                {
                    dir = ListSortDirection.Descending;
                    break;
                }
                default:
                {
                    return false;
                }
            }
            Sort(column, dir);
            return true;
        }

        /// <summary>
        /// 修改当前选中项
        /// </summary>
        /// <param name="update">修改动作</param>
        public void UpdateCurrSelectedItem(UpdateElementEvent update)
        {
            UpdateItemAt(SelectedItemIndex, update);
        }

        /// <summary>
        /// 修改指定项
        /// </summary>
        /// <param name="idx">项在列表中的索引</param>
        /// <param name="update">修改动作</param>
        public void UpdateItemAt(int idx, UpdateElementEvent update)
        {
            T t = DataList[idx];
            update(ref t);
            if(!t.Equals(DataList[idx]))
            {
                DataList[idx] = t; // 如果委托中使用new引用了新的对象，则更新成新的对象
            }
            Refresh();
        }
    }

    /// <summary>
    /// 绑定数据源的DataGridView的列
    /// </summary>
    public sealed class BindingDataGridViewColumn
    {
        /// <summary>
        /// 数据类型，默认DataGridViewTextBoxColumn
        /// </summary>
        public Type Type{ get; set; } = typeof(DataGridViewTextBoxColumn);

        /// <summary>
        /// 列名及绑定数据属性名
        /// </summary>
        public string NameAndDataPropertyName{ get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string HeaderText{ get; set; }

        /// <summary>
        /// 可见
        /// </summary>
        public bool Visible{ get; set; } = true;

        /// <summary>
        /// 排序模式，默认DataGridViewColumnSortMode.Automatic
        /// </summary>
        public DataGridViewColumnSortMode SortMode{ get; set; } = DataGridViewColumnSortMode.Automatic;
    }
}