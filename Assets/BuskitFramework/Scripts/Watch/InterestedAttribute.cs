﻿/*******************************************************************************
* 版权所有：北京润尼尔网络科技有限公司
* 版本声明：v1.0.0
* 项目名称：Com.Rainier.Buskit3D
* 类 名 称：LogicBehaviour
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
    /// 感兴趣的Entity类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InterestedAttribute : Attribute
    {
        /// <summary>
        /// 类型列表
        /// </summary>
        public Type[] Types { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">注解类型</param>
        public InterestedAttribute(Type type)
        {
            Types = new Type[] { type };
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterestedAttribute()
        {

        }
    }
}
