/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ACUseObjectBase
* 创建日期：2022-05-13 11:46:10
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SystemManager;
using App;
using Misc;

namespace Actor
{
    /// <summary>
    /// 
    /// </summary>
	public class ACUseObjectBase : MonoBehaviour 
	{
		public List<StepIndex> UseStepIndex = new List<StepIndex>();
		protected CompMouse cm;
		protected StepSystem stepSystem;
		protected RaycastHit hit;

		protected virtual void Awake()
		{
			cm = gameObject.AddComponent<CompMouse>();
			stepSystem = AppFacade.Instance.stepSystem;
		}

		public virtual void DoAnimation()
		{
			
		}
	}
}

