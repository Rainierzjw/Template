/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIEventDrag
* 创建日期：2022-11-11 19:50:44
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace UI
{
    public class UIEventDrag : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
    {
        private Vector2 lastPos;

        public event Action<UIEventDrag> onBeginDrag;

        public event Action<UIEventDrag> onEndDrag;

        public event Action<UIEventDrag, Vector2> onDrag;

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.onBeginDrag?.Invoke(this);
            lastPos = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            this.onDrag?.Invoke(this, eventData.position - lastPos);
            lastPos = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.onEndDrag?.Invoke(this);
        }

        public void Dispose()
        {
            this.onBeginDrag = null;
            this.onEndDrag = null;
            this.onDrag = null;
            UnityEngine.Object.Destroy(this);
        }
    }
}

