/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：GameObjectPool
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：场景中的所有GameObject
* 修改记录：
* 日期 描述 更新功能
* 
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 场景中的所有GameObject
    /// </summary>
    public class GameObjectPool : ObjectPool<GameObject>
    {
        /// <summary>
        /// 在所有GameObject中搜索唯一ID的GameObject
        /// </summary>
        /// <param name="uniqueId">模型对象的唯一id</param>
        /// <returns>GameObject</returns>
        public GameObject FindGameObject(int uniqueId)
        {
            GameObject go = null;
            this.Foreach((i, t) =>
            {
                if(t.GetComponent<UniqueID>() != null)
                {
                    UniqueID uid = t.GetComponent<UniqueID>();
                    if(uid.UniqueId == uniqueId)
                    {
                        go = t;
                        return;
                    }
                }
            });
            return go;
        }
    }
}
