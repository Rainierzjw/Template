/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：EventSource
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 事件源或事件侦听器集合，拥有事件侦听器的管理
    /// 和事件广播，可以向事件侦听器发送属性事件（PropertyEvent）
    /// 和方法事件（MethodEvent）
    /// </summary>
    public class EventSource : IEventSource
    {
        /// <summary>
        /// 事件侦听器个数
        /// </summary>
        public int Count {
            get
            {
                return _listeners.Count;
            }
        }

        /// <summary>
        /// 事件侦听器列表
        /// </summary>
        private List<IEventListener> _listeners = null;

        /// <summary>
        /// 事件源对象，通常情况下这个对象是实现了
        /// IEventSourceSupportable接口的对象
        /// </summary>
        private object _eventSource = null;

        /// <summary>
        /// 事件广播是否有效的标志
        /// </summary>
        private bool _enabled = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sourceObject">事件源对象</param>
        public EventSource(object sourceObject)
        {
            if (sourceObject == null)
            {
                throw new Buskit3DException("no event source object is specified");            
            }
            _eventSource = sourceObject;
            _listeners = new List<IEventListener>();
        }

        /// <summary>
        /// 添加事件侦听器
        /// </summary>
        /// <param name="listener">实现了IEventListener的对象</param>
        public void AddEventListener(IEventListener listener)
        {
            //如果已经包含事件侦听器则直接退出
            if (this._listeners.Contains(listener))
            {
                return;
            }

            //如果当前广播事件功能关闭，则直接将侦听器加入侦听器列表后退出
            if (!this._enabled)
            {
                this._listeners.Add(listener);
                return;
            }

            //广播初始化事件，在_eventSource中搜索所有带有
            //“ForeFireEvent”注解的属性和字段
            //并广播需要广播初始化消息的属性或字段事件
            var allFields = this._eventSource
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(o => o.GetCustomAttributes(typeof(FireInitEventAttribute), true).Length > 0)
                .ToArray();
            var allProperties = this._eventSource
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(o => o.GetCustomAttributes(typeof(FireInitEventAttribute), true).Length > 0)
                .ToArray();


            this._listeners.Add(listener);

            //向Listener广播字段初始化消息
            foreach (FieldInfo info in allFields)
            {
                 
                    var value = info.GetValue(this._eventSource);
                    PropertyEvent evt = new PropertyInitEvent();
                    evt.EventName = info.Name;//attr.EventName;
                    evt.EventSource = this._eventSource;
                    evt.OldValue = value;
                    evt.NewValue = value;
                    listener.OnEvent(evt);
               
            }

            //向Listener广播初属性始化消息
            foreach (PropertyInfo info in allProperties)
            {
                    var value = info.GetValue(this._eventSource);
                    PropertyEvent evt = new PropertyInitEvent();
                    evt.EventName = info.Name;
                    evt.EventSource = this._eventSource;
                    evt.OldValue = value;
                    evt.NewValue = value;
                    listener.OnEvent(evt);
               
            }
           
        }

        /// <summary>
        /// 清理事件侦听器
        /// </summary>
        public void ClearListeners()
        {
            this._listeners.Clear();
        }

        /// <summary>
        /// 是否包含事件侦听器
        /// </summary>
        /// <param name="listener">侦听器</param>
        /// <returns></returns>
        public bool Contains(IEventListener listener)
        {
            return _listeners.Contains(listener);
        }

        /// <summary>
        /// 删除事件侦听器
        /// </summary>
        /// <param name="listener">侦听器</param>
        public void RemoveEventListener(IEventListener listener)
        {
            if (this._listeners.Contains(listener))
            {
                this._listeners.Remove(listener);
            }
        }

        /// <summary>
        /// 广播事件
        /// </summary>
        /// <param name="evt">消息体</param>
        public void FireEvent(IEvent evt)
        {
            //根据广播事件有效性设置判断广播事件还是丢弃事件
            //在_enabled为false的情况下丢弃事件，_enabled为true的情况下广播事件
            if (this._enabled) 
            {
                evt.EventSource = this._eventSource;
                foreach(IEventListener listener in _listeners)
                {
                    listener.OnEvent(evt);
                }
            }
        }

        /// <summary>
        /// 关闭广播
        /// </summary>
        public void OffFire()
        {
            this._enabled = false;
        }

        /// <summary>
        /// 开启广播
        /// </summary>
        public void OnFire()
        {
            this._enabled = true;
        }
    }
}
