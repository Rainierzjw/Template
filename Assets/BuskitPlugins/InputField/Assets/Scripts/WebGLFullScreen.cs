/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：WebGLFullScreen
* 创建日期：2020-03-16 15:15:20
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Com.Rainier.Buskit3D.WebGLInputField
{
    /// <summary>
    /// WebGL全屏/退出全屏接口
    /// </summary>
    public class WebGLFullScreen
    {
        /// <summary>
        /// 全屏Js引用
        /// </summary>
        [DllImport("__Internal")]
        private static extern void UnityFullScreen();

        /// <summary>
        /// 退出全屏Js引用
        /// </summary>
        [DllImport("__Internal")]
        private static extern void UnitySmallScreen();

        /// <summary>
        /// 全屏按钮接口
        /// </summary>
        public static void WebFullScreen()
        {
            try
            {
                UnityFullScreen();
            }
            catch (EntryPointNotFoundException e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// 全屏按钮接口
        /// </summary>
        public static void WebSmallScreen()
        {
            try
            {
                UnitySmallScreen();
            }
            catch (EntryPointNotFoundException e)
            {
                Debug.LogError(e);
            }
        }
    }
}

