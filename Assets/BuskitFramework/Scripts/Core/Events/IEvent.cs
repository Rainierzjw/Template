/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：IEvent
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 描述事件的基本信息
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 发送事件的对象
        /// </summary>
        object EventSource { set; get; }

        /// <summary>
        /// 一般是一个属性或方法的名称
        /// </summary>
        string EventName { set; get; }
    }
}

