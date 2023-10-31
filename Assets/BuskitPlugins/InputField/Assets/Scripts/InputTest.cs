/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：InputTest
* 创建日期：2020-03-16 15:15:20
* 作者名称：黎特为
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Rainier.Buskit3D.WebGLInputField
{
    /// <summary>
    /// WebGL屏幕控制示例
    /// </summary>
    public class InputTest : MonoBehaviour
    {
        /// <summary>
        /// 全屏
        /// </summary>
        public void FullScreen()
        {
            WebGLFullScreen.WebFullScreen();
        }

        /// <summary>
        /// 非全屏
        /// </summary>
        public void SmallScreen()
        {
            WebGLFullScreen.WebSmallScreen();
        }
    }
}
