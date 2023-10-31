﻿/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： EmptyEnumerator
* 创建日期：2022-03-29 15:42:36
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;

namespace Misc
{
    /// <summary>
    /// 
    /// </summary>
    public class EmptyEnumerator : IEnumerator
    {
	    public bool MoveNext() => false;

	    public void Reset()
	    {
	    }

	    public object Current => (object) null;
    }
}

