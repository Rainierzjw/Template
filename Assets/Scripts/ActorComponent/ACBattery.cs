/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ACBattery
* 创建日期：2022-04-11 10:47:08
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using App;
using Misc;
using Extend;
using UnityEngine.EventSystems;
using HighlightingSystem;
using UI;
using SceneComponent;

namespace Actor
{
    /// <summary>
    /// 
    /// </summary>
    public class ACBattery : ACUseObjectBase
    {
        protected Vector3 originPos;
        protected Vector3 originRot;
        protected Collider collider;
        private Highlighter highlighter;
        protected override void Awake()
        {
            base.Awake();
            collider = transform.GetComponent<Collider>();
            highlighter = transform.GetComponent<Highlighter>();
            originPos = transform.localPosition;
            originRot = transform.localEulerAngles;
            cm.onStartDrag += OnStartDrag;
            cm.onDrag += OnDrag;
            cm.onEndDrag += OnEndDrag;
            cm.onEnter += OnEnter;
            cm.onExit += OnExit;

            AppFacade.Instance.stepSystem.Step.onChange += OnStepChange;
        }

        private void OnExit(CompMouse obj)
        {
            
        }

        private void OnEnter(CompMouse obj)
        {
            if ((stepSystem.Step.data < 1018 || stepSystem.Step.data > 2009)&& stepSystem.Step.data<3000)
            {
                //根据零件名判断显示的文字窗口文字
                switch (transform.name)
                {
                    
                }

            }
        }

        /// <summary>
        ///根据步骤判断物体的collider的启用
        /// </summary>
        /// <param name="step"></param>
        private void OnStepChange(int step)
        {
            if (UseStepIndex.Contains((StepIndex)step))
            {
                //collider.enabled = true;
                highlighter.enabled = true;
            }
            else
            {
                //collider.enabled = false;
                highlighter.enabled = false;
            }
        }

        private void OnDestroy()
        {
            cm.onStartDrag -= OnStartDrag;
            cm.onDrag -= OnDrag;
            cm.onEndDrag -= OnEndDrag;
            AppFacade.Instance.stepSystem.Step.onChange -= OnStepChange;
        }



        private void OnStartDrag(CompMouse cm)
        {
            if (UseStepIndex.Contains((StepIndex)stepSystem.Step.data))
            {
                collider.enabled = false;
            }
        }

        private void OnDrag(CompMouse cm)
        {
            if (UseStepIndex.Contains((StepIndex)stepSystem.Step.data))
            {
                if (CameraCtrl.Instance.MouseHit(out hit, "Aim"))
                {
                    if (hit.transform.name == transform.name)
                    {
                        transform.position = hit.transform.position;
                        transform.eulerAngles = hit.transform.eulerAngles;
                    }
                    else
                    {
                        transform.position = CameraCtrl.Instance.cam.transform.position +
                                             CameraCtrl.Instance.cam.ScreenPointToRay(Input.mousePosition).direction * 1;
                    }
                }
                else
                {
                    transform.position = CameraCtrl.Instance.cam.transform.position +
                                         CameraCtrl.Instance.cam.ScreenPointToRay(Input.mousePosition).direction * 1;
                }
            }
        }

        private void OnEndDrag(CompMouse cm)
        {
            if (CameraCtrl.Instance.MouseHit(out hit, "Aim"))
            {
                if (hit.transform.name == transform.name)
                {
                    if (UseStepIndex.Contains((StepIndex)stepSystem.Step.data))
                    {
                        transform.position = hit.transform.position;
                        transform.eulerAngles = hit.transform.eulerAngles;
                        //如果在安装过程中，安装完毕取消碰撞体
                        //collider.enabled = false;
                        hit.transform.gameObject.SetActive(false);
                        AppFacade.Instance.stepSystem.AddStep();
                        if (stepSystem.Step.data >= 2000)
                        {
                            transform.SetParent(transform.parent.parent.parent.FindTnf("ClearTools"));
                        }
                    }
                    else
                    {
                        collider.enabled = true;
                        transform.localPosition = originPos;
                        transform.localEulerAngles = originRot;
                    }
                }
                else
                {
                    collider.enabled = true;
                    transform.localPosition = originPos;
                    transform.localEulerAngles = originRot;
                }
            }
            else
            {
                collider.enabled = true;
                transform.localPosition = originPos;
                transform.localEulerAngles = originRot;
            }
        }

    }
}