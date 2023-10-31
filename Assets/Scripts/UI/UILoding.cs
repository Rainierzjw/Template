/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UILoding
* 创建日期：2022-03-04 13:21:58
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SystemManager;
using App;
using Com.Rainier.Buskit.Unity.Architecture.Logging;
using Com.Rainier.Buskit3D;
using Misc;

namespace UI
{
    /// <summary>
    /// 
    /// </summary>
	public class UILoding : BaseUI
    {
	    private List<GameObject> images = new List<GameObject>();
	    private float time;
	    private int index;
	    private float progress;
	    private AsyncOperation async;
	    
	    /// <summary>
	    /// MVVM上下文环境
	    /// </summary>
	    MvvmContext context;

	    /// <summary>
	    /// 获取MVVM上下文环境
	    /// </summary>
	    protected override void OnLoaded()
	    {
		    base.OnLoaded();
		    context = GetComponent<MvvmContext>();
		    for (int i = 0; i < transform.childCount-1; i++)
		    {
			    images.Add(transform.GetChild(i).gameObject);
		    }
	    }

	    protected override void OnShow()
	    {
		    base.OnShow();
		    StartCoroutine(LoadScene());
	    }

	    protected override void OnHide()
	    {
		    time = 0;
		    index = 0;
		    progress = 0;
		    base.OnHide();
	    }

	    private void Update()
	    {
		    time += Time.deltaTime;
		    if (time > 0.2f)
		    {
			    time = 0;
			    if (index % 5 == 0)
			    {
				    foreach (var image in images)
				    {
					    image.gameObject.SetActive(false);
				    }
			    }
			    else
			    {
				    transform.Find((index % 5).ToString()).gameObject.SetActive(true);
			    }
			    index++;
		    }

		    // 进度条预留
		    if (progress < 100)
		    {
			    progress++;
		    }
		    else
		    {
			    async.allowSceneActivation = true;
		    }
	    }
	    
	    /// <summary>
	    /// 异步加载场景
	    /// </summary>
	    /// <returns></returns>
	    IEnumerator LoadScene()
	    {
		    SceneSystem ss = AppFacade.Instance.sceneSystem;
		    async = ss.EnterScene(GlobalParameter.EnterScene);
		    async.allowSceneActivation = false;
		    Debug.Log("进入场景" + GlobalParameter.EnterScene);
		    yield return async;
		    Hide();
	    }
    }
}

