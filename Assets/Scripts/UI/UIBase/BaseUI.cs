/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： BaseUI
* 创建日期：2022-02-24 13:06:42
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：UI基类（模板）
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
    public class BaseUI : UIBase
    {
        private readonly List<UIBase> childrenList = new List<UIBase>();

        private readonly Dictionary<string, UIBase> childrenDic = new Dictionary<string, UIBase>();

        protected BaseUI parent { get; private set; }

        public RectTransform rectTransform => transform as RectTransform;

        public virtual T LoadChild<T>(string name, bool show = true, object data = null) where T : BaseUI, new()
        {
            if (!loaded)
            {
                Debug.LogError("not loaded");
                return default;
            }

            var tnf = FindChildTnf(name);
            if (tnf == null)
            {
                Debug.LogError("no child");
                return default;
            }

            var child = tnf.gameObject.AddComponent<T>();
            childrenList.Add(child);
            childrenDic.Add(name, child);            
            child.ChildLoad();
            child.SetParent(this);
            if (show)
            {
                child.Show(data);
            }
            else
            {
                child.Hide();
            }

            return child;
        }


        public void LoadChild(string name, BaseUI child, bool show = true, object data = null)
        {
            if (!loaded)
            {
                Debug.LogError("not loaded");
                return;
            }

            var tnf = FindChildTnf(name);
            if (tnf == null)
            {
                return;
            }

            childrenList.Add(child);
            childrenDic.Add(name, child);
            child.ChildLoad();
            child.SetParent(this);
            if (show)
            {
                child.Show(data);
            }
            else
            {
                child.Hide();
            }
        }

        public virtual UIBase LoadChild(string name, Type type, bool show = true, object data = null)
        {
            if (!typeof(UIBase).IsAssignableFrom(type))
            {
                Debug.LogWarning($"{type} not implement IUI");
                return null;
            }

            if (!loaded)
            {
                Debug.LogError("not loaded");
                return null;
            }

            var tnf = FindChildTnf(name);
            if (tnf == null)
            {
                return null;
            }

            var child = (BaseUI)Activator.CreateInstance(type);
            childrenList.Add(child);
            childrenDic.Add(name, child);
            child.ChildLoad();
            child.SetParent(this);
            if (show)
            {
                child.Show(data);
            }
            else
            {
                child.Hide();
            }

            return child;
        }

        public T GetChild<T>(string name) where T : UIBase
        {
            if (childrenDic.ContainsKey(name)) return (T)childrenDic[name];

            Debug.LogWarning($"not exist child {name}");
            return default(T);
        }

        public UIBase GetChild(string name)
        {
            if (childrenDic.ContainsKey(name)) return childrenDic[name];

            Debug.LogWarning($"not exist child {name}");
            return null;
        }

        public virtual void UnloadChild<T>(string name) where T : UIBase
        {
            var child = GetChild<T>(name);
            if (child == null) return;
            childrenList.Remove(child);
            childrenDic.Remove(name);
            child.ChildUnload();
        }

        public virtual void UnloadChild(string name)
        {
            var child = GetChild(name);
            if (child == null) return;
            childrenList.Remove(child);
            childrenDic.Remove(name);
            child.ChildUnload();
        }

        public virtual void UnloadChild(UIBase child)
        {
            if (!childrenList.Contains(child))
            {
                Debug.LogWarning($"not exist {transform.name}");
                return;
            }

            childrenList.Remove(child);
            childrenDic.Remove(transform.name);
            child.ChildUnload();
        }

        public void EveryChild(Action<UIBase> filter)
        {
            foreach (var child in childrenList)
            {
                filter(child);
            }
        }

        public void SetParent(BaseUI parent)
        {
            if (parent == null)
            {
                Debug.LogWarning("parent is null");
                return;
            }

            if (!parent.loaded)
            {
                Debug.LogWarning("parent not loaded");
                return;
            }

            if (!loaded)
            {
                Debug.LogWarning("not loaded");
                return;
            }

            if (this.parent == null)
            {
                Debug.LogWarning("set parent");
                this.parent = parent;
                return;
            }

            Debug.LogWarning("reset parent");
            var tnf = this.transform;
            parent.UnloadChild(this);

            tnf.SetParent(parent.transform);
            parent.LoadChild(transform.name, this);
        }
    }
}