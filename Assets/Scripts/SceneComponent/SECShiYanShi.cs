/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： SECShiYanShi
* 创建日期：2022-04-20 19:57:16
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using App;
using UI;
using Misc;
using Extend;

namespace SceneComponent
{
    /// <summary>
    /// 
    /// </summary>
	public class SECShiYanShi : MonoSingleton<SECShiYanShi>
    {
        public GameObject dianChi_Zhuang;
        public GameObject cloneCorpo_Zhuang;
        public GameObject cloneCorpo_Chai;
        public GameObject dianChi_Chai;
        public GameObject dianChi_Ce;
        public GameObject luoShuan;
        /// <summary>
        /// 真空表
        /// </summary>
        public Transform zhenKongBiao;
        public Transform player;

        public Camera camera;
        /// <summary>
        /// 清洁仪器盖子
        /// </summary>
        public GameObject CleaningInstrumentLid;
        /// <summary>
        /// 清洁仪器启动按钮
        /// </summary>
        public Transform CleaningInstrumentStartBtn;
        /// <summary>
        ///  清洁时间
        /// </summary>
        public int clearTime;
        /// <summary>
        /// 清洁次数
        /// </summary>
        public int clearNum;
        /// <summary>
        /// 判断是否是第一次打开场景，是否需要加载ui 
        /// </summary>
        private static bool isShowUI = true;
        private void Awake()
        {
            camera = player.FindComp<Camera>("Camera");
            //如果是第一次打开就加载ui，如果点击按钮重新加载打开，重新加载场景
            //TODO:根据阶段数据类里面存储的玩家位置信息加载玩家位置
            if (isShowUI == true) ShowUI();
            
        }

        private void ShowUI()
        {
            var ui = AppFacade.Instance.uiSystem;
            isShowUI = false;
        }

        //private void OnDisable()
        //{
        //	UnloadUI();
        //}

        //UI的注册和注销放在AppFacade脚本中进行注册
        //private void LoadUI()
        //{
        //	var ui = AppFacade.Instance.uiSystem;
        //	ui.LoadUI<UITitle>().Show();
        //	ui.LoadUI<UIMenu>().Show();
        //	ui.LoadUI<UIZongHeRenZhi>();
        //	ui.LoadUI<UIRanLiaoDianChi>();
        //	ui.LoadUI<UIBeiJing>();
        //	ui.LoadUI<UIMuDi>();
        //	ui.LoadUI<UIYuanLi>();
        //	ui.LoadUI<UIToolsPanel>();
        //}

        //private void UnloadUI()
        //{
        //	var ui = AppFacade.Instance.uiSystem;
        //	ui.UnloadUI<UITitle>();
        //	ui.UnloadUI<UIMenu>();
        //	ui.UnloadUI<UIZongHeRenZhi>();
        //	ui.UnloadUI<UIRanLiaoDianChi>();
        //	ui.UnloadUI<UIBeiJing>();
        //	ui.UnloadUI<UIMuDi>();
        //	ui.UnloadUI<UIYuanLi>();
        //	ui.UnloadUI<UIToolsPanel>();
        //}
    }
}

