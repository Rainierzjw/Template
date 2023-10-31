/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：LogicBehaviour
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 事件处理器
    /// </summary>
    public class LogicBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 处理属性或方法事件
        /// </summary>
        /// <param name="evt">消息体</param>
        public virtual void ProcessLogic(IEvent evt)
        {

        }
    }
}
