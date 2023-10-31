/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SceneSystem
* 创建日期：2022-02-24 09:48:34
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：场景系统
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using App;
using Misc;
using UnityEngine.SceneManagement;

namespace SystemManager
{
    /// <summary>
    /// 
    /// </summary>
	public class SceneSystem : ISystem 
	{
		private AppFacade app;

		public SceneSystem(AppFacade appFacade)
		{
			app = appFacade;
		}
		
		public void Register()
		{
			app.RegisterSystem(SystemType.SceneSystem, this);
		}

		public void UnRegister()
		{
			app.UnRegisterSystem(SystemType.SceneSystem);
		}
		
		/// <summary>
		/// 进入场景
		/// </summary>
		/// <param name="sceneName"></param>
		public AsyncOperation EnterScene(SceneName sceneName)
		{
			return SceneManager.LoadSceneAsync(sceneName.ToString());
		}
	}
}

