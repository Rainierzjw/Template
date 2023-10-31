
/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UniqueID
* 创建日期：2019-03-20 13:26:57
* 作者名称：王庚
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 唯一ID
    /// </summary>
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
	public class UniqueID : MonoBehaviour 
	{
        /// <summary>
        /// 动态ID的起始值，最小值为静态Id的最大值+1
        /// </summary>
        private static int runtimeOriginId;

        /// <summary>
        /// 唯一id生成器
        /// </summary>
        [SerializeField]
        private ObjectIdentity objectIdentity = new ObjectIdentity();

        /// <summary>
        /// 手动标记当前物体是不是预制体
        /// </summary>
        public bool isPrefab = false;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static UniqueID()
        {
            runtimeOriginId = (1<< 16)>>1;
        }

        /// <summary>
        /// 唯一id
        /// </summary>
        [HideInInspector]
        public int UniqueId
        {
            get
            {
                if (isPrefab)
                {
                    return objectIdentity.prefabId;
                }
                else
                {
                    return objectIdentity.id;
                }
            }
            set
            {
                objectIdentity.id = (short)value;
            }
        }

        /// <summary>
        /// UnityMethod
        /// </summary>
        private void Awake()
        {
           
            if (isPrefab)
            {
                runtimeOriginId += 1;
                objectIdentity.prefabId = runtimeOriginId;
            }

            ObjectIdentity.RegisterIdentity(this.objectIdentity);
        }

        /// <summary>
        /// UnityMethod
        /// </summary>
        private void OnDestroy()
        {
            ObjectIdentity.UnregisterIdentity(this.objectIdentity);
        }
    }
}

