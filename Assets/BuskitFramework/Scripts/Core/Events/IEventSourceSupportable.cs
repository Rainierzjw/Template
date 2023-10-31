/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：IEventSourceSupportable
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 描述一个支持事件源的对象
    /// </summary>
    public interface IEventSourceSupportable
    {
        /// <summary>
        /// 添加事件侦听器
        /// </summary>
        /// <param name="listener">侦听器</param>
        void AddEventListener(IEventListener listener);

        /// <summary>
        /// 删除事件侦听器
        /// </summary>
        /// <param name="listener">侦听器</param>
        void RemoveEventListener(IEventListener listener);

        /// <summary>
        /// 清楚事件侦听器列表
        /// </summary>
        void ClearListeners();

        /// <summary>
        /// 广播事件
        /// </summary>
        /// <param name="evt">属性变化消息体</param>
        void FireEvent(PropertyEvent evt);

        /// <summary>
        /// 广播事件
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        void FireEvent(string name, object oldValue, object newValue);

        /// <summary>
        /// 广播事件
        /// </summary>
        /// <param name="evt">方法调用消息体</param>
        void FireEvent(MethodEvent evt);

        /// <summary>
        /// 判断是否已经拥有事件源
        /// </summary>
        /// <param name="listener">侦听器</param>
        /// <returns></returns>
        bool Contains(IEventListener listener);

        /// <summary>
        /// 广播事件
        /// </summary>
        /// <param name="name">方法名</param>
        /// <param name="args">参数列表</param>
        void FireEvent(string name, object[] args);

        /// <summary>
        /// 强制发送所有属性事件
        /// </summary>
        void ForceFireAll();

        /// <summary>
        /// 开启事件广播
        /// </summary>
        void OnFire();

        /// <summary>
        /// 关闭事件广播
        /// </summary>
        void OffFire();
    }
}
