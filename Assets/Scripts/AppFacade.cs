/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： AppFacade
* 创建日期：2022-04-07 14:07:17
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
using Misc;
using UI;

namespace App
{
    /// <summary>
    /// 
    /// </summary>
	public class AppFacade : MonoSingleton<AppFacade>
    {
        private Dictionary<SystemType, ISystem> _DicManager = new Dictionary<SystemType, ISystem>();

        public Transform canvas;

        private void Awake()
        {
            canvas = transform.Find("Canvas");
            UISystem uis = new UISystem(this);
            uis.Register();
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            SceneSystem scs = new SceneSystem(this);
            scs.Register();
            StepSystem step = new StepSystem(this);
            step.Register();
            CameraPathSystem camera = new CameraPathSystem(this);
            camera.Register();
            LoadUI();
        }

        private void OnDisable()
        {
            UnloadUI();
            DestroySystem();
        }

        #region 属性

        public UISystem uiSystem
        {
            get { return (UISystem)GetSystemByName(SystemType.UISystem); }
        }

        public StepSystem stepSystem
        {
            get { return (StepSystem)GetSystemByName(SystemType.StepSystem); }
        }

        public SceneSystem sceneSystem
        {
            get { return (SceneSystem)GetSystemByName(SystemType.SceneSystem); }
        }

        public SceneSystem cameraPathSystem
        {
            get { return (SceneSystem)GetSystemByName(SystemType.CameraPathSystem); }
        }


        #endregion

        #region 公开方法

        public Coroutine BeginCoroutine(IEnumerator coroutine)
        {
            return this.StartCoroutine(coroutine);
        }

        public void Delay(float seconds, Action action)
        {
            StartCoroutine(Coroutine(seconds, action));
        }

        /// <summary>
        /// 注册
        /// </summary>
        public void RegisterSystem(SystemType type, ISystem system)
        {
            _DicManager.Add(type, system);
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void UnRegisterSystem(SystemType type)
        {
            _DicManager.Remove(type);
        }

        #endregion

        #region 私有方法

        IEnumerator Coroutine(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }

        private void LoadUI()
        {
            uiSystem.LoadUI<UIHome>().Show();
            uiSystem.LoadUI<UILoding>();
        }

        private void UnloadUI()
        {
            uiSystem.UnloadUI<UIHome>();
            uiSystem.UnloadUI<UILoding>();
        }

        /// <summary>
        /// 根据名称获取管理器
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns></returns>
        private ISystem GetSystemByName(SystemType systemName)
        {
            ISystem ism = null;
            if (_DicManager.ContainsKey(systemName))
            {
                ism = _DicManager[systemName];
            }
            else
            {
                Debug.LogError($"当前系统{systemName}还没注册！！");
            }

            return ism;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        private void DestroySystem()
        {
            _DicManager.Clear();
        }

        #endregion
    }
}

