/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIBase
* 创建日期：2022-11-12 13:50:01
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：UI基类
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using System.Threading;
using Com.Rainier.Buskit3D;
using Extend;
using Misc;
using System.Collections.Generic;

namespace UI
{
    /// <summary>
    /// UI基类
    /// </summary>
	public class UIBase : MonoBehaviour
    {
        public enum UILayer
        {
            Default,
            Top,
            PLC,
            Mask,
            Loding,
        }

        protected object data { get; private set; }

        public bool visible { get; private set; }

        public UILayer layer = UILayer.Default;

        public event Action onLoaded;

        public event Action onUnloaded;

        public event Action onShow;

        public event Action onHide;

        public bool loaded { get; private set; }


        private void Awake()
        {
            onLoaded += OnLoaded;
            onUnloaded += OnUnloaded;
            onShow += OnShow;
            onHide += OnHide;
        }

        protected virtual void OnLoaded() { }

        protected virtual void OnUnloaded() { }

        protected virtual void OnShow() { }
        protected virtual void OnHide() { }

        public void Load()
        {
            loaded = true;
            onLoaded?.Invoke();
            switch (layer)
            {
                case UILayer.Default:
                    transform.SetParent(transform.parent.FindTnf2("Default"));
                    break;
                case UILayer.Top:
                    transform.SetParent(transform.parent.FindTnf2("Top"));
                    break;
                case UILayer.PLC:
                    transform.SetParent(transform.parent.FindTnf2("PLC"));
                    break;
                case UILayer.Mask:
                    transform.SetParent(transform.parent.FindTnf2("Mask"));
                    break;
                case UILayer.Loding:
                    transform.SetParent(transform.parent.FindTnf2("Loding"));
                    break;
            }
            gameObject.SetActive(false);
        }

        public void Unload()
        {
            loaded = false;
            onUnloaded?.Invoke();
            onLoaded -= OnLoaded;
            onUnloaded -= OnUnloaded;
            onShow -= OnShow;
            onHide -= OnHide;
        }

        public void Show(object data = null)
        {
            this.data = data;
            visible = true;
            if (loaded && !gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                onShow?.Invoke();
            }
        }

        public void Hide()
        {
            if (loaded && gameObject.activeSelf)
            {
                visible = false;
                onHide?.Invoke();
                gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            Unload();
        }

        protected Transform FindChildTnf(string name)
        {
            return transform.FindRTnf2(name);
        }

        public void ChildLoad()
        {
            loaded = true;
            onLoaded?.Invoke();
        }

        public void ChildUnload()
        {
            loaded = false;
            onUnloaded?.Invoke();
        }
    }
}

