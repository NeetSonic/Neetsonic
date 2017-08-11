using System;
using System.Collections.Generic;

namespace Neetsonic.DataStructure
{
    /// <summary>
    /// 缓存池结构
    /// Neetsonic
    /// 2017.8.11
    /// </summary>
    /// <typeparam name="TKey">对象的键</typeparam>
    /// <typeparam name="TVal">对象的值</typeparam>
    public sealed class CachePool<TKey, TVal>
    {
        private readonly LinkedList<KeyValuePair<TKey, TVal>> _pool = new LinkedList<KeyValuePair<TKey, TVal>>();
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="size">缓存池大小</param>
        public CachePool(int size) => Size = size;

        /// <summary>
        /// 缓冲区大小
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// 根据键，获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值，若不存在返回默认值</returns>
        public TVal this[TKey key] => GetVal(key);

        /// <summary>
        /// 根据键，获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值，若不存在返回默认值</returns>
        public TVal GetVal(TKey key)
        {
            if(key == null)
            {
                throw new ArgumentNullException("key");
            }

            LinkedListNode<KeyValuePair<TKey, TVal>> target = null;
            LinkedListNode<KeyValuePair<TKey, TVal>> curr = _pool.First;
            while(curr != _pool.Last && curr != null)
            {
                if(curr.Value.Key.Equals(key))
                {
                    target = curr;
                    break;
                }
                curr = curr.Next;
            }

            // 没找到，返回默认值
            if(null == target)
            {
                return default(TVal);
            }

            // 找到，缓存到第一位，返回值
            _pool.Remove(target);
            _pool.AddFirst(target);
            return target.Value.Value;
        }
        /// <summary>
        /// 添加新的键值对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        public void Add(TKey key, TVal val)
        {
            _pool.AddFirst(new KeyValuePair<TKey, TVal>(key, val));
            while(_pool.Count > Size) // 超出缓存池容量，移除最后一个
            {
                // 先释放非托管资源，再移除 
                IDisposable dis = _pool.Last.Value.Value as IDisposable;
                if(null != dis)
                {
                    dis.Dispose();
                }
                _pool.RemoveLast();
            }
        }
        /// <summary>
        /// 清空缓存池
        /// </summary>
        public void Clear() => _pool.Clear();
    }
}