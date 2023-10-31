/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CompMouse
* 创建日期：2022-04-11 11:30:11
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using App;
using UnityEngine.EventSystems;

namespace Misc
{
    /// <summary>
    /// 
    /// </summary>
	public class CompMouse : UnityEngine.EventSystems.EventTrigger
	{
		private void Update()
		{
			OnDrag();
		}

		#region UIEvent

		public delegate void DelHandler(CompMouse cm); //定义委托类型

		public DelHandler onUIClick; //鼠标点击
		public DelHandler onUIDown; //鼠标按下
		public DelHandler onUIUp; //鼠标抬起
		public DelHandler onUIEnter; //鼠标进入
		public DelHandler onUIExit; //鼠标退出
		public DelHandler onUIStartDrag; //开始拖拽
		public DelHandler onUIDrag;//正在拖拽
		public DelHandler onUIEndDrag;//结束拖拽
		private bool isDrag;
		
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (onUIClick != null) onUIClick(this);
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			if (onUIDown != null)
			{
				onUIDown(this);
			}

			if (onUIStartDrag != null)
			{
				isDrag = true;
				onUIStartDrag(this);
			} 
		}

		public override void OnPointerEnter(PointerEventData eventData)
		{
			if (onUIEnter != null)
			{
				onUIEnter(this);
			}
		}

		public override void OnPointerExit(PointerEventData eventData)
		{
			if (onUIExit != null)
			{
				onUIExit(this);
			}
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			if (onUIUp != null)
			{
				onUIUp(this);
			}

			if (onUIEndDrag != null)
			{
				isDrag = false;
				onUIEndDrag(this);
			}
		}

		private void OnDrag()
		{
			if (isDrag && onUIDrag != null)
			{
				onUIDrag(this);
			}
		}
		
		#endregion

		#region ObjEvent

		public event Action<CompMouse> onStartDrag;
		public event Action<CompMouse> onDrag;
		public event Action<CompMouse> onEndDrag;
		public event Action<CompMouse> onEnter;
		public event Action<CompMouse> onExit;
		public event Action<CompMouse> onDouble;
		public event Action<CompMouse> onDown;
		public event Action<CompMouse> onUp;

		public float durtion = 0.5f;
        
		private float lastTime;
        
		public string key { get; set; }
        
		private void OnMouseDown()
		{
			if (onDown != null)
			{
				onDown(this);
			}
			else
			{
				OnStartMouseDrag();
			}
		}

		private void OnMouseUp()
		{
			if (Time.time - lastTime < durtion)
			{
				if (onDouble != null)
				{
					onDouble(this);
				}
			}
			else
			{
				if (onUp != null)
				{
					onUp(this);
				}
				else
				{
					OnEndMouseDrag();
				}
			}

			lastTime = Time.time;
		}

		private void OnStartMouseDrag()
		{
			if (EventSystem.current.IsPointerOverGameObject()) return;
			onStartDrag?.Invoke(this);
		}
		
		private void OnMouseDrag()
		{
			if (EventSystem.current.IsPointerOverGameObject()) return;
			onDrag?.Invoke(this);
		}
		
		private void OnEndMouseDrag()
		{
			if (EventSystem.current.IsPointerOverGameObject()) return;
			onEndDrag?.Invoke(this);
		}

		private void OnMouseEnter()
		{
			if (EventSystem.current.IsPointerOverGameObject()) return;
			onEnter?.Invoke(this);
		}

		private void OnMouseExit()
		{
			if (EventSystem.current.IsPointerOverGameObject()) return;
			onExit?.Invoke(this);
		}

		#endregion
		
		

		private void OnDestroy()
		{
			onUIUp = null;
			onUIDown = null;
			onUIExit = null;
			onUIEnter = null;
			onUIClick = null;
			onDown = null;
			onUp = null;
			onDouble = null;
			onStartDrag = null;
			onDrag = null;
			onEndDrag = null;
			onEnter = null;
			onExit = null;
		}
	}
}

