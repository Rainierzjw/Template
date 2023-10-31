/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： IField
* 创建日期：2022-04-11 09:23:46
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;

namespace Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IField<T>
    {
	    T data { get; set; }

	    event Action<T> onChange;

	    void Set(T data);

	    void Call();

	    void Dispose();
    }
}

