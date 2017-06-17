using System.ComponentModel;
using System.Windows.Forms;

namespace Neetsonic.Tool
{
    /// <summary>
    /// 通用转换器
    /// Neetsonic
    /// 2017.6.18
    /// </summary>
    public static class Convertor
    {
        /// <summary>
        /// 将SortOrder转换为等价的ListSortDirection
        /// </summary>
        /// <param name="order">SortOrder对象</param>
        /// <returns>等价的ListSortDirection对象</returns>
        public static ListSortDirection SortOrderToListSortDirection(SortOrder order)
        {
            return order == SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }
    }
}