using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Neetsonic.Tool;

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
        public BindingDataGridView()
        {
            InitializeComponent();
            Init();
        }

        private DataStructure.BindingList<T> _dataList; // 绑定的数据源列表
        private readonly List<DataGridViewColumn> LinkColumns = new List<DataGridViewColumn>(); // 链接类型的列

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
                if(null != sortedColumn)
                {
                    Sort(sortedColumn, Convertor.SortOrderToListSortDirection(oldSortOrder));
                }

                // 还原选中项
                if(default(T).Equals(oldSelectedItem))
                {
                    for(int idx = 0; idx < DataList.Count; idx++)
                    {
                        if(IsTheSameItem(DataList[idx], oldSelectedItem))
                        {
                            DataGridViewRow theRow = Rows[idx];
                            theRow.Selected = true;
                            if(!theRow.Displayed)
                            {
                                FirstDisplayedScrollingRowIndex = idx;
                            }
                            break;
                        }
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
                if(OpenLinkInBrowser && -1 != args.RowIndex && LinkColumns.Contains(Columns[args.ColumnIndex]))
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
        public void SetColumns(IEnumerable<BindingDataGridViewColumn> columns)
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
                    LinkColumns.Add(col);
                }
            }
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
        public bool Visible{ get; set; }

        /// <summary>
        /// 排序模式，默认DataGridViewColumnSortMode.Automatic
        /// </summary>
        public DataGridViewColumnSortMode SortMode{ get; set; } = DataGridViewColumnSortMode.Automatic;
    }
}