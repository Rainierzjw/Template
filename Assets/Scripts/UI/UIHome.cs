/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIHome
* 创建日期：2022-04-20 10:24:11
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using App;
using Extend;
using Misc;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 
    /// </summary>
	public class UIHome : BaseUI
    {
	    private Button startBtn;

	    protected override void OnLoaded()
	    {
		    base.OnLoaded();
		    startBtn = transform.FindComp<Button>("startBtn");
            startBtn = transform.FindTnf2("startBtn").GetComponent<Button>();
		    startBtn.onClick.AddListener(StartClick);
	    }

	    private void StartClick()
	    {
		    Hide();
		    GlobalParameter.EnterScene = SceneName.ShiYanShi;
		    AppFacade.Instance.uiSystem.GetUI<UILoding>().Show();
	    }
    }
}

