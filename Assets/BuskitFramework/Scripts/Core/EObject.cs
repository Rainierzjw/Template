/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：EObject
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：作为非Buskit3D基本对象使用，其他非Unity对象均可继承自此对象
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using System.Linq;
using System.Reflection;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 作为非Buskit3D基本对象使用，其他非Unity对象均可继承自此对象
    /// </summary>
    public class EObject : IEventSourceSupportable
    {
        /// <summary>
        /// 实体有效性描述
        /// </summary>
        [FireInitEvent]
        public bool Enable
        {
            get
            {
                return this._enable;
            }
            set
            {
                var oldValue = this._enable;
                this._enable = value;
                this.FireEvent("Enable", oldValue, this._enable);
            }
            
        }
        /// <summary>
        /// 实体有效性描述
        /// </summary>
        protected bool _enable = true;

        /// <summary>
        /// 事件源用于记录实体对象的侦听器、广播属性和方法事件
        /// </summary>
        EventSource eventSource = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EObject()
        {
            this.eventSource = new EventSource(this);
        }

        /// <summary>
        /// 添加事件侦听器
        /// </summary>
        /// <param name="listener">侦听器</param>
        public void AddEventListener(IEventListener listener)
        {
            if (!Enable)
            {
                return;
            }

            this.eventSource.AddEventListener(listener);
        }

        /// <summary>
        /// 清除事件侦听器
        /// </summary>
        public void ClearListeners()
        {
            if (!Enable)
            {
                return;
            }

            this.eventSource.ClearListeners();
        }

        /// <summary>
        /// 广播属性事件
        /// </summary>
        /// <param name="evt">属性变化消息体</param>
        public void FireEvent(PropertyEvent evt)
        {
            if (!Enable)
            {
                return;
            }
            this.eventSource.FireEvent(evt);
        }

        /// <summary>
        /// 广播属性事件
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        public void FireEvent(string name, object oldValue, object newValue)
        {
            if (!Enable)
            {
                return;
            }
            PropertyEvent pe = new PropertyEvent();
            pe.EventName = name;
            pe.OldValue = oldValue;
            pe.NewValue = newValue;
            this.eventSource.FireEvent(pe);
        }

        /// <summary>
        /// 广播方法事件
        /// </summary>
        /// <param name="evt">方法调用消息体</param>
        public void FireEvent(MethodEvent evt)
        {
            if (!Enable)
            {
                return;
            }
            this.eventSource.FireEvent(evt);
        }

        /// <summary>
        /// 广播方法事件
        /// </summary>
        /// <param name="name">方法名</param>
        /// <param name="args">参数列表</param>
        public void FireEvent(string name, object[] args)
        {
            if (!Enable)
            {
                return;
            }

            MethodEvent me = new MethodEvent();
            me.EventName = name;
            if (args != null)
            {
                me.Argments = new object[args.Length];
                args.CopyTo(me.Argments, 0);
            }
            else
            {
                me.Argments = new object[0];
            }

            this.eventSource.FireEvent(me);
        }

        /// <summary>
        /// 关闭广播事件功能
        /// </summary>
        public void OffFire()
        {
            if (!Enable)
            {
                return;
            }

            this.eventSource.OffFire();
        }

        /// <summary>
        /// 打开广播事件功能
        /// </summary>
        public void OnFire()
        {
            if (!Enable)
            {
                return;
            }

            this.eventSource.OnFire();
        }

        /// <summary>
        /// 删除事件侦听器
        /// </summary>
        /// <param name="listener">侦听器</param>
        public void RemoveEventListener(IEventListener listener)
        {
            if (!Enable)
            {
                return;
            }

            this.eventSource.RemoveEventListener(listener);
        }

        /// <summary>
        /// 判断是否已经拥有事件源
        /// </summary>
        /// <param name="listener">侦听器</param>
        /// <returns></returns>
        public bool Contains(IEventListener listener)
        {
            return this.eventSource.Contains(listener);
        }

        /// <summary>
        /// 强制发送所有属性事件
        /// </summary>
        public void ForceFireAll()
        {
            //广播初始化事件，在_eventSource中搜索所有带有
            //“FireInitEvent”注解的属性和字段
            //并广播需要广播初始化消息的属性或字段事件
            var allFields = this
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(o => o.GetCustomAttributes(typeof(FireInitEventAttribute), true).Length > 0)
                .ToArray();
            var allProperties = this
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(o => o.GetCustomAttributes(typeof(FireInitEventAttribute), true).Length > 0)
                .ToArray();

            //向Listener广播字段初始化消息
            foreach (FieldInfo info in allFields)
            {
                var value = info.GetValue(this);
                var attr = info.GetCustomAttribute<FireInitEventAttribute>();
                this.FireEvent(info.Name, value, value);
            }

            //向Listener广播初属性始化消息
            foreach (PropertyInfo info in allProperties)
            {
                var value = info.GetValue(this);
                var attr = info.GetCustomAttribute<FireInitEventAttribute>();
                this.FireEvent(info.Name, value, value);
            }
        }

    }
}
