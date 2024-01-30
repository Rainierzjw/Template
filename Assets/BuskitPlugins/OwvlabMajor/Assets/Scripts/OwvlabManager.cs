/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： OwvlabManager
* 创建日期：2022-03-30 16:59:40
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using Misc;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D.OwvlabMajor
{
    /// <summary>
    /// 
    /// </summary>
    public class OwvlabManager : MonoSingleton<OwvlabManager>
    {
        private Owvlab owvlab;
        public OwvlabContext owvlabContext;

        private void Awake()
        {
            owvlab = gameObject.AddComponent<Owvlab>();
#if UNITY_WEBGL
            owvlabContext = owvlab.Init();
#endif
        }
    }
}