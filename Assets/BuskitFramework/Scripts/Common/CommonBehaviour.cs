/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：CommonBehaviour
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：作为其他CommonBehaviour的基类使用,预先注入了一些基本单例
*           EventSource、StorageSystem、AssociatedRecordPool，方便程序员使用
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using Com.Rainier.Buskit.Unity.Architecture.Injector;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 作为其他Behaviour的基类使用
    /// </summary>
    public class CommonBehaviour : EventSupportBehaviour
    {
        /// <summary>
        /// 事件驱动Behaviour对象池
        /// </summary>
        [Inject]
        protected DataModelPool dataModels;

        /// <summary>
        /// 注入关联表
        /// </summary>
        [Inject]
        protected AssociatedRecordPool associatedTable;

        /// <summary>
        /// 注入GameObject对象池
        /// </summary>
        [Inject]
        protected GameObjectPool gameObjects;
    }
}
