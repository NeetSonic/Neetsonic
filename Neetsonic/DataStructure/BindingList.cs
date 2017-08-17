using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Neetsonic.DataStructure
{
    /// <summary>
    /// 支持排序、查找等功能的绑定列表
    /// Neetsonic
    /// 2017.6.16
    /// </summary>
    /// <typeparam name="T">列表元素类型</typeparam>
    public abstract class BindingList<T> : System.ComponentModel.BindingList<T>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        protected BindingList() => Init();

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="lst">构造列表</param>
        protected BindingList(IList<T> lst) : base(lst) => Init();

        /// <summary>
        /// 绑定的控件调用此方法判断列表是否支持排序
        /// </summary>
        protected override bool SupportsSortingCore => true;

        /// <summary>
        /// 查找第一个符合要求的元素索引
        /// </summary>
        /// <param name="match">需要满足的要求</param>
        /// <returns>第一个符合要求的元素索引，不存在则返回-1</returns>
        public int FindItemIndex(Predicate<T> match)
        {
            for(int idx = 0; idx < Items.Count; idx++)
            {
                if(match(Items[idx]))
                {
                    return idx;
                }
            }
            return -1;
        }
        /// <summary>
        /// 绑定的控件调用此方法进行列表的排序，之后自动重新绑定回控件
        /// </summary>
        /// <param name="prop">要排序的属性</param>
        /// <param name="direction">排序顺序</param>
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction) => (Items as List<T>)?.Sort((x, y) => Cmp(prop, direction, x, y));
        /// <summary>
        /// 用于排序时比较两个元素的方法
        /// </summary>
        /// <param name="property">要排序的属性</param>
        /// <param name="direction">排序顺序</param>
        /// <param name="x">用于比较的元素</param>
        /// <param name="y">用于比较的元素</param>
        /// <returns>比较结果</returns>
        protected abstract int Cmp(PropertyDescriptor property, ListSortDirection direction, T x, T y);
        /// <summary>
        /// 初始化工作
        /// </summary>
        private void Init()
        {
            AllowNew = true;
            AddingNew += (sender, args) => args.NewObject = default(T);
        }
    }
}