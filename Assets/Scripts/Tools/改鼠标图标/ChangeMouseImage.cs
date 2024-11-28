/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： ChangeMouseImage
* 创建日期：2023-09-26 15:06:38
* 作者名称：韦伟
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace Com.Rainier.WeiWei
{
    /// <summary>
    /// 
    /// </summary>
	public class ChangeMouseImage : MonoBehaviour 
	{
        public Texture2D cursorTexture_Normal;
        public Texture2D cursorTexture_MouseDown;
        //public float x;
        //public float y;
        void Start()
        {
            Cursor.SetCursor(cursorTexture_Normal, new Vector2(26,20), CursorMode.Auto);
        }
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Cursor.SetCursor(cursorTexture_MouseDown, new Vector2(26, 20), CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(cursorTexture_Normal, new Vector2(26, 20), CursorMode.Auto);
            }
        }
	}
}

