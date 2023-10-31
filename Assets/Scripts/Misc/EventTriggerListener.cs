/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： EventTriggerListener
* 创建日期：2022-03-07 08:57:27
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace App
{
    /// <summary>
    /// 
    /// </summary>
    public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger
    {
        public delegate void DelHandler(GameObject go); //定义委托类型

        public DelHandler onClick; //鼠标点击
        public DelHandler onDown; //鼠标按下
        public DelHandler onUp; //鼠标抬起
        public DelHandler onEnter; //鼠标进入
        public DelHandler onExit; //鼠标退出
        public DelHandler onSelect; //选中对象
        public DelHandler onUpdateSelect; //选中对象每帧调用
        public DelHandler onSubmit;

        /// <summary>
        /// 得到监听器组件
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static EventTriggerListener Get(GameObject go)
        {
            EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
            if (listener == null) listener = go.AddComponent<EventTriggerListener>();
            return listener;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null) onClick(gameObject);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("onDown的值维"+onDown);
            if (onDown != null)
            {
                Debug.Log("gameObject"+gameObject);
                onDown(gameObject);
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (onEnter != null)
            {
                onEnter(gameObject);
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (onExit != null)
            {
                onExit(gameObject);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (onUp != null)
            {
                Debug.Log("onUp事件执行");
                onUp(gameObject);
            }
        }

        public override void OnSelect(BaseEventData eventBaseData)
        {
            if (onSelect != null)
            {
                onSelect(gameObject);
            }
        }

        public override void OnUpdateSelected(BaseEventData eventBaseData)
        {
            if (onUpdateSelect != null)
            {
                onUpdateSelect(gameObject);
            }
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            if (onSubmit != null)
            {
                onSubmit(gameObject);
            }
        }
    }
}