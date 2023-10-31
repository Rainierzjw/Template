/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CameraPathSystem
* 创建日期：2022-04-11 08:41:51
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using App;
using Misc;

namespace SystemManager
{
    /// <summary>
    /// 
    /// </summary>
	public class CameraPathSystem : ISystem 
	{
		private AppFacade app;
		public CameraCtrl camera;
		
		public CameraPathSystem(AppFacade appFacade)
		{
			app = appFacade;
		}
		
		public void Register()
		{
			app.RegisterSystem(SystemType.CameraPathSystem, this);
		}

		public void UnRegister()
		{
			app.UnRegisterSystem(SystemType.CameraPathSystem);
		}

		public void Init()
		{
			camera = GameObject.FindWithTag("Player").GetComponent<CameraCtrl>();
		}
	}
}

