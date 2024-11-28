/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： Field
* 创建日期：2022-04-11 09:22:13
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
    [Serializable]
    public class Field<T> : IField<T>
    {
	    [SerializeField]
	    private T _data;

	    public Field() => this._data = default (T);

	    public Field(T data) => this._data = data;

	    public T data
	    {
		    get => this._data;
		    set
		    {
			    if ((object) value == null)
			    {
				    if ((object) this._data == null)
					    return;
			    }
			    else if (value.Equals((object) this._data))
				    return;
			    this._data = value;
			    this.Call();
		    }
	    }

	    public event Action<T> onChange;

	    public void Set(T data) => this._data = data;

	    public void Call()
	    {
		    Action<T> onChange = this.onChange;
		    if (onChange == null)
			    return;
		    onChange(this._data);
	    }

	    public void Dispose() => this.onChange = (Action<T>) null;

	    public static implicit operator T(Field<T> field) => field._data;

	    public static implicit operator Field<T>(T val) => new Field<T>(val);
    }
}

