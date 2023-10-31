/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： StepSystem
* 创建日期：2022-04-08 13:33:28
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using Actor;
using Extend;
using App;
using Data;
using Misc;
using SceneComponent;
using DG.Tweening;
using UI;

namespace SystemManager
{
    /// <summary>
    /// 
    /// </summary>
	public class StepSystem : ISystem
    {
        private AppFacade app;
        private Field<int> step = new Field<int>();

        public Field<int> Step
        {
            get => step;
        }

        public StepSystem(AppFacade appFacade)
        {
            app = appFacade;
            Init();
        }

        public void Register()
        {
            app.RegisterSystem(SystemType.StepSystem, this);
        }

        public void UnRegister()
        {
            app.UnRegisterSystem(SystemType.StepSystem);
        }

        public void Init()
        {
            step.onChange += OnStepChange;
            step.data = (int)StepIndex.s0_0;
        }

        public void AddStep()
        {
            step.data++;
        }

        public void SetStep(int index)
        {
            step.data = index;
        }

        private void OnStepChange(int index)
        {
            switch (index)
            {
                
            }
        }
    }
}

