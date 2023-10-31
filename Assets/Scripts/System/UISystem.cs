/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UISystem
* 创建日期：2022-02-24 11:38:48
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：UI系统
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemManager;
using App;
using Misc;
using UI;

namespace SystemManager
{
    /// <summary>
    /// 
    /// </summary>
	public class UISystem : ISystem 
	{
		private AppFacade app;
		
		private Dictionary<string, GameObject> loadUIDic = new Dictionary<string, GameObject>();
		
		public UISystem(AppFacade appFacade)
		{
			app = appFacade;
		}
		
		public void Register()
		{
			app.RegisterSystem(SystemType.UISystem, this);
		}
		
		public void UnRegister()
		{
			app.UnRegisterSystem(SystemType.UISystem);
		}
		
		public T LoadUI<T>(Transform tnf = null, Action onComplete = null) where T : BaseUI => (T) this.LoadUI(typeof (T), tnf, onComplete);
		private BaseUI LoadUI(Type type, Transform tnf, Action onComplete)
		{
			if (!loadUIDic.Keys.Contains(type.Name))
			{
				GameObject uiPreb = Resources.Load(GlobalParameter.GetUIPrefabPath(type.Name), typeof(GameObject)) as GameObject;
				var ui =  GameObject.Instantiate(uiPreb, tnf == null ? app.canvas : tnf);
				loadUIDic.Add(type.Name, ui);
				var uicpt = ui.GetComponent<BaseUI>();
				if(onComplete != null)
					uicpt.onLoaded += onComplete;
				uicpt.Load();
				return uicpt;
			}
			else
			{
				Debug.LogError("该UI已加载");
				return loadUIDic[type.Name].GetComponent<BaseUI>();
			}
		}

		public T GetUI<T>() where T : BaseUI => (T) GetUI(typeof (T));

		private BaseUI GetUI(Type type)
		{
			if (!loadUIDic.Keys.Contains(type.Name))
			{
				Debug.LogError("该UI未加载");
				return null;
			}
			else
			{
				return loadUIDic[type.Name].GetComponent<BaseUI>();
			}
		}

		public void UnloadUI<T>(Action onComplete = null) where T : BaseUI => UnloadUI(typeof(T), onComplete);

		private void UnloadUI(Type type, Action onComplete)
		{
			if (!loadUIDic.Keys.Contains(type.Name))
			{
				Debug.LogError("该UI已卸载");
			}
			else
			{
				var ui = loadUIDic[type.Name];
				if(onComplete != null)
					ui.GetComponent<BaseUI>().onUnloaded += onComplete;
				loadUIDic.Remove(type.Name);
				GameObject.Destroy(ui);
			}
		}
	}
}

