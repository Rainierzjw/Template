/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ETransform
* 创建日期：2022-04-11 08:57:33
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;

namespace Extend
{
    /// <summary>
    /// 
    /// </summary>
    public static class ETransform
    {
        public static Transform FindTnf(this Transform parent, string name, bool recursive = true)
        {
            int childCount = parent.childCount;
            for (int index = 0; index < childCount; ++index)
            {
                Transform child = parent.GetChild(index);
                if (child.name == name)
                    return child;
                if (recursive)
                {
                    Transform tnf = child.FindTnf(name, recursive);
                    if ((UnityEngine.Object) tnf != (UnityEngine.Object) null)
                        return tnf;
                }
            }

            return (Transform) null;
        }

        public static Transform FindTnf2(this Transform parent, string name, bool recursive = true) =>
            new List<Transform>((IEnumerable<Transform>) parent.GetComponentsInChildren<Transform>()).FindTnf2(name,
                recursive);

        public static RectTransform FindRTnf2(
            this Transform parent,
            string name,
            bool recursive = true)
        {
            return parent.FindComp<RectTransform>(name, recursive);
        }

        public static Transform FindTnf2(
            this IEnumerable<Transform> tranforms,
            string name,
            bool recursive = true)
        {
            List<Transform> transformList = new List<Transform>(tranforms);
            int index1 = 0;
            while (index1 < transformList.Count)
            {
                Transform transform = transformList[index1];
                if (transform.name == name)
                    return transform;
                ++index1;
                if (recursive)
                {
                    int childCount = transform.childCount;
                    for (int index2 = 0; index2 < childCount; ++index2)
                        transformList.Add(transform.GetChild(index2));
                }
            }

            return (Transform) null;
        }

        /// <summary>
        /// 获取组件，没有会添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tnf"></param>
        /// <returns></returns>
        public static T GetComp<T>(this Transform tnf) where T : Component
        {
            if (tnf.GetComponent<T>()) return tnf.GetComponent<T>();
            else return tnf.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 按<类型>("名称")查找组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static T FindComp<T>(this Transform parent, string name, bool recursive = true)
        {
            Transform tnf2 = parent.FindTnf2(name, recursive);
            return (UnityEngine.Object) tnf2 == (UnityEngine.Object) null ? default(T) : tnf2.GetComponent<T>();
        }

        public static Component FindComp(
            this Transform parent,
            System.Type type,
            string name,
            bool recursive = true)
        {
            Transform tnf2 = parent.FindTnf2(name, recursive);
            return (UnityEngine.Object) tnf2 == (UnityEngine.Object) null ? (Component) null : tnf2.GetComponent(type);
        }

        public static bool AnyChild(this Transform target, Predicate<Transform> filter, bool recursive = true)
        {
            int childCount = target.childCount;
            for (int index = 0; index < childCount; ++index)
            {
                Transform child = target.GetChild(index);
                if (filter(child))
                    return true;
                if (recursive)
                    child.AnyChild(filter, recursive);
            }

            return false;
        }

        public static void EveryChild(this Transform target, Action<Transform> filter, bool recursive = true)
        {
            int childCount = target.childCount;
            for (int index = 0; index < childCount; ++index)
            {
                Transform child = target.GetChild(index);
                filter(child);
                if (recursive)
                    child.EveryChild(filter, recursive);
            }
        }

        public static bool EveryChild(
            this Transform target,
            Func<Transform, bool> filter,
            bool recursive = true)
        {
            int childCount = target.childCount;
            for (int index = 0; index < childCount; ++index)
            {
                Transform child = target.GetChild(index);
                if (!filter.Invoke(child))
                    return false;
                if (recursive)
                    child.EveryChild(filter, recursive);
            }

            return true;
        }

        public static T FindParentComponent<T>(this Transform obj, bool recursive = true)
        {
            if ((UnityEngine.Object) obj == (UnityEngine.Object) null)
                return default(T);
            Transform transform = obj;
            do
            {
                T component = transform.GetComponent<T>();
                if ((object) component != null)
                    return component;
                if (recursive)
                    transform = transform.parent;
                else
                    break;
            } while ((UnityEngine.Object) transform != (UnityEngine.Object) null);

            return default(T);
        }

        public static string PathTo(this Transform tnf, Transform parent)
        {
            Transform parent1 = tnf.parent;
            string str = "";
            while (true)
            {
                if (!((UnityEngine.Object) parent1 == (UnityEngine.Object) null))
                {
                    if (!((UnityEngine.Object) parent1 == (UnityEngine.Object) parent))
                    {
                        str = parent1.name + "/" + str;
                        parent1 = parent1.parent;
                    }
                    else
                        goto label_3;
                }
                else
                    break;
            }

            return (string) null;
            label_3:
            return str.Length > 0 ? str.Substring(0, str.Length - 1) : str;
        }

        public static Transform FindWithPath(this Transform tnf, string path)
        {
            if (path == "")
                return tnf;
            string[] strArray = path.Split('/');
            Transform parent = tnf;
            foreach (string name in strArray)
            {
                parent = parent.FindTnf2(name);
                if ((UnityEngine.Object) parent == (UnityEngine.Object) null)
                    return (Transform) null;
            }

            return parent;
        }

        public static Vector3 RelativePositionTo(this Transform tnf, Transform target) =>
            tnf.position - target.position;

        public static void DoMoveAnchoredPosition(this Transform tnf, Vector2 pos, float duration)
        {
            RectTransform rectTnf = tnf.GetComponent<RectTransform>();
            if ((UnityEngine.Object) rectTnf == (UnityEngine.Object) null)
                throw new ArgumentException("target " + tnf.name + " not exist RectTransform");
            DOTween.To((DOGetter<Vector2>) (() => rectTnf.anchoredPosition),
                (DOSetter<Vector2>) (v => rectTnf.anchoredPosition = v), pos, duration);
        }
    }
}