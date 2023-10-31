/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：Buskit3DException
* 创建日期：2016/11/13 下午 05:03:58
* 作者名称: 王志远
* 功能描述：
* 修改记录：
* 日期 描述 更新功能
******************************************************************************/

using System;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 简单的Buskit3D自定义异常
    /// </summary>
    public class Buskit3DException : Exception
    {
        /// <summary>
        /// 简单的Buskit3D自定义异常
        /// </summary>
        /// <param name="msg">异常消息</param>
        public Buskit3DException(string msg) : base(msg)
        {

        }
    }
}
