/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：IEventSource
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 事件源接口
    /// </summary>
    public interface IEventSource
    {
        /// <summary>
        /// 事件监听器个数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 清除事件侦听器列表
        /// </summary>
        void ClearListeners();

        /// <summary>
        /// 判断是否已经拥有事件源
        /// </summary>
        /// <param name="listener">侦听器</param>
        /// <returns></returns>
        bool Contains(IEventListener listener);

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
        /// 广播事件
        /// </summary>
        /// <param name="evt">消息体</param>
        void FireEvent(IEvent evt);

        /// <summary>
        /// 关闭事件广播
        /// </summary>
        void OffFire();

        /// <summary>
        /// 开启事件广播
        /// </summary>
        void OnFire();
    }
}

