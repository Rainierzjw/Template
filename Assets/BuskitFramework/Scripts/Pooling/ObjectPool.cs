/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：InjectAttribute
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using System.Collections.Generic;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 简单对象池
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    public class ObjectPool<T> where T : class
    {
        /// <summary>
        /// 查找回到
        /// </summary>
        /// <param name="i">index值</param>
        /// <param name="t">目标类型对象</param>
        public delegate void ForeachCallback(int i, T t);

        /// <summary>
        /// 对象集合
        /// </summary>
        protected List<T> objects = new List<T>();

        /// <summary>
        /// 注册对象
        /// </summary>
        /// <param id="obj">目标类型</param>
        public virtual void RegisterObject(T obj)
        {
            objects.Add(obj);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param id="obj">目标类型</param>
        public virtual void RemoveObject(T obj)
        {
            objects.Remove(obj);
        }

        /// <summary>
        /// 遍历对象池
        /// </summary>
        /// <param id="cb">遍历回调</param>
        public void Foreach(ForeachCallback cb)
        {
            if (cb == null)
            {
                return;
            }
            int i = 0;
            foreach (T obj in objects)
            {
                cb(i, obj);
                i++;
            }
        }

        /// <summary>
        /// 查找特定类型的对象
        /// </summary>
        /// <typeparam name="T1">目标类型</typeparam>
        /// <returns>对象集合</returns>
        public T1[] FindObjects<T1>() where T1 : T
        {
            List<T1> list = new List<T1>();
            foreach (T item in objects)
            {
                if (item is T1)
                {
                    T1 tt = (T1)item;
                    list.Add(tt);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 查找T1类型的对象
        /// </summary>
        /// <typeparam name="T1">目标类型</typeparam>
        /// <returns>目标对象</returns>
        public T1 FindObject<T1>() where T1 : T
        {
            foreach (T item in objects)
            {
                if (item is T1)
                {
                    T1 tt = (T1)item;
                    return tt;
                }
            }
            return default(T1);
        }

        /// <summary>
        /// 获取大小
        /// </summary>
        /// <returns>数量</returns>
        public int Count()
        {
            return objects.Count;
        }

        /// <summary>
        /// 获取第几个对象
        /// </summary>
        /// <param id="index"></param>
        /// <returns>对象</returns>
        public T Get(int index)
        {
            return objects[index];
        }

        /// <summary>
        /// 清除所有对象
        /// </summary>
        public virtual void OnDestroy()
        {
            this.objects.Clear();
        }

        /// <summary>
        /// 清除所有数据
        /// </summary>
        public virtual void Clear()
        {
            this.objects.Clear();
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <returns>数据集合</returns>
        public T[] ToArray()
        {
            return objects.ToArray();
        }
    }
}

