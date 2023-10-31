/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：IEventListener
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 事件监听器
    /// </summary>
    public interface IEventListener
    {
        /// <summary>
        /// 当发生新事件时触发此函数
        /// </summary>
        /// <param name="evt">消息体</param>
        void OnEvent(IEvent evt);
    }
}
