/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： WriteOnce
* 创建日期：2022-11-11 19:44:25
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
	public class WriteOnce<T>
    {
        private T _data;

        public bool empty { get; private set; }

        public T data
        {
            get
            {
                if (!empty)
                {
                    return _data;
                }

                Debug.LogError("value not set");
                return default(T);
            }
            set
            {
                if (empty)
                {
                    _data = value;
                    empty = false;
                }
                else
                {
                    Debug.LogError("value already set");
                }
            }
        }

        public WriteOnce()
        {
            empty = true;
        }

        public static implicit operator T(WriteOnce<T> val)
        {
            return val._data;
        }
    }
}

